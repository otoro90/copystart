using System.Text.Json;
using CopyStart.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CopyStart.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var problemDetails = new ProblemDetails
        {
            Instance = context.Request.Path
        };

        switch (exception)
        {
            case ValidationException validationEx:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Validation Failed";
                problemDetails.Detail = "One or more validation errors occurred.";
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                problemDetails.Extensions["errors"] = validationEx.Errors.Select(e => new
                {
                    Property = e.PropertyName,
                    Error = e.ErrorMessage
                });
                break;

            case InvalidStateTransitionException stateEx:
                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                problemDetails.Title = "Invalid State Transition";
                problemDetails.Detail = stateEx.Message;
                problemDetails.Type = "https://tools.ietf.org/html/rfc4918#section-11.2";
                problemDetails.Extensions["currentState"] = stateEx.CurrentState;
                problemDetails.Extensions["targetState"] = stateEx.TargetState;
                break;

            case DomainException domainEx:
                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                problemDetails.Title = "Domain Rule Violation";
                problemDetails.Detail = domainEx.Message;
                problemDetails.Type = "https://tools.ietf.org/html/rfc4918#section-11.2";
                break;

            case KeyNotFoundException notFoundEx:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                problemDetails.Status = StatusCodes.Status404NotFound;
                problemDetails.Title = "Resource Not Found";
                problemDetails.Detail = notFoundEx.Message;
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4";
                break;

            default:
                _logger.LogError(exception, "An unhandled exception occurred.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Title = "Internal Server Error";
                problemDetails.Detail = "An unexpected error occurred.";
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
                break;
        }

        context.Response.ContentType = "application/problem+json";
        await JsonSerializer.SerializeAsync(context.Response.Body, problemDetails);
    }
}
