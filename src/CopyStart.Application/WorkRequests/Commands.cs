using CopyStart.Domain.Common;
using CopyStart.Application.Common;
using CopyStart.Domain.Entities;
using CopyStart.Domain.Exceptions;
using CopyStart.Domain.Repositories;
using FluentValidation;

namespace CopyStart.Application.WorkRequests;

public record CreateWorkRequestCommand(
    Guid CustomerId,
    string Description,
    string ContactName,
    string ContactPhone,
    Guid? AssetId = null,
    string Actor = "staff");

public class CreateWorkRequestValidator : AbstractValidator<CreateWorkRequestCommand>
{
    public CreateWorkRequestValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.").MaximumLength(2000);
        RuleFor(x => x.ContactName).NotEmpty().WithMessage("ContactName is required.").MaximumLength(200);
    }
}

public class CreateWorkRequestHandler
{
    private readonly IWorkRequestRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITenantContext _tenantContext;

    public CreateWorkRequestHandler(IWorkRequestRepository repository, IUnitOfWork unitOfWork, ITenantContext tenantContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _tenantContext = tenantContext;
    }

    public async Task<WorkRequestDto> HandleAsync(CreateWorkRequestCommand command, CancellationToken ct)
    {
        var request = new WorkRequest(
            tenantId: _tenantContext.GetTenantId(),
            customerId: command.CustomerId,
            description: command.Description,
            contactName: command.ContactName,
            contactPhone: command.ContactPhone,
            assetId: command.AssetId,
            actor: command.Actor);

        await _repository.AddAsync(request, ct);
        await _unitOfWork.CommitAsync(ct);

        return WorkRequestDto.FromDomain(request);
    }
}

public record AssignWorkRequestCommand(Guid Id, string TechnicianUserId, string Actor = "staff");

public class AssignWorkRequestValidator : AbstractValidator<AssignWorkRequestCommand>
{
    public AssignWorkRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("WorkRequestId is required.");
        RuleFor(x => x.TechnicianUserId).NotEmpty().WithMessage("TechnicianUserId is required.");
    }
}

public class AssignWorkRequestHandler
{
    private readonly IWorkRequestRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public AssignWorkRequestHandler(IWorkRequestRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<WorkRequestDto> HandleAsync(AssignWorkRequestCommand command, CancellationToken ct)
    {
        var request = await _repository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Work request with ID '{command.Id}' was not found.");

        request.Assign(command.TechnicianUserId, command.Actor);
        await _repository.UpdateAsync(request, ct);
        await _unitOfWork.CommitAsync(ct);

        return WorkRequestDto.FromDomain(request);
    }
}

public record StartWorkRequestServiceCommand(Guid Id, string Actor = "staff");

public class StartWorkRequestServiceValidator : AbstractValidator<StartWorkRequestServiceCommand>
{
    public StartWorkRequestServiceValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("WorkRequestId is required.");
    }
}

public class StartWorkRequestServiceHandler
{
    private readonly IWorkRequestRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public StartWorkRequestServiceHandler(IWorkRequestRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<WorkRequestDto> HandleAsync(StartWorkRequestServiceCommand command, CancellationToken ct)
    {
        var request = await _repository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Work request with ID '{command.Id}' was not found.");

        request.StartService(command.Actor);
        await _repository.UpdateAsync(request, ct);
        await _unitOfWork.CommitAsync(ct);

        return WorkRequestDto.FromDomain(request);
    }
}

public record CompleteWorkRequestCommand(Guid Id, string Actor = "staff");

public class CompleteWorkRequestValidator : AbstractValidator<CompleteWorkRequestCommand>
{
    public CompleteWorkRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("WorkRequestId is required.");
    }
}

public class CompleteWorkRequestHandler
{
    private readonly IWorkRequestRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteWorkRequestHandler(IWorkRequestRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<WorkRequestDto> HandleAsync(CompleteWorkRequestCommand command, CancellationToken ct)
    {
        var request = await _repository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Work request with ID '{command.Id}' was not found.");

        request.Complete(command.Actor);
        await _repository.UpdateAsync(request, ct);
        await _unitOfWork.CommitAsync(ct);

        return WorkRequestDto.FromDomain(request);
    }
}

public record CancelWorkRequestCommand(Guid Id, string Reason, string Actor = "staff");

public class CancelWorkRequestValidator : AbstractValidator<CancelWorkRequestCommand>
{
    public CancelWorkRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("WorkRequestId is required.");
        RuleFor(x => x.Reason).NotEmpty().MaximumLength(500).WithMessage("Reason is required.");
    }
}

public class CancelWorkRequestHandler
{
    private readonly IWorkRequestRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CancelWorkRequestHandler(IWorkRequestRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<WorkRequestDto> HandleAsync(CancelWorkRequestCommand command, CancellationToken ct)
    {
        var request = await _repository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Work request with ID '{command.Id}' was not found.");

        request.Cancel(command.Reason, command.Actor);
        await _repository.UpdateAsync(request, ct);
        await _unitOfWork.CommitAsync(ct);

        return WorkRequestDto.FromDomain(request);
    }
}
