using CopyStart.Domain.Common;
using CopyStart.Domain.Entities;
using CopyStart.Domain.Exceptions;
using CopyStart.Domain.Repositories;
using FluentValidation;

namespace CopyStart.Application.WorkOrders;

public record CreateWorkOrderCommand(
    Guid WorkRequestId,
    string TechnicianUserId,
    string? DiagnosticNotes = null,
    string Actor = "staff");

public class CreateWorkOrderValidator : AbstractValidator<CreateWorkOrderCommand>
{
    public CreateWorkOrderValidator()
    {
        RuleFor(x => x.WorkRequestId).NotEmpty().WithMessage("WorkRequestId is required.");
        RuleFor(x => x.TechnicianUserId).NotEmpty().WithMessage("TechnicianUserId is required.");
    }
}

public class CreateWorkOrderHandler
{
    private readonly IWorkOrderRepository _repository;
    private readonly IWorkRequestRepository _requestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWorkOrderHandler(
        IWorkOrderRepository repository,
        IWorkRequestRepository requestRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _requestRepository = requestRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<WorkOrderDto> HandleAsync(CreateWorkOrderCommand command, CancellationToken ct)
    {
        var request = await _requestRepository.GetByIdAsync(command.WorkRequestId, ct)
            ?? throw new KeyNotFoundException($"Work request with ID '{command.WorkRequestId}' was not found.");

        var order = new WorkOrder(
            workRequestId: command.WorkRequestId,
            technicianUserId: command.TechnicianUserId,
            diagnosticNotes: command.DiagnosticNotes,
            actor: command.Actor);

        await _repository.AddAsync(order, ct);
        await _unitOfWork.CommitAsync(ct);

        return WorkOrderDto.FromDomain(order);
    }
}

public record StartWorkOrderCommand(Guid Id, string Actor = "staff");

public class StartWorkOrderValidator : AbstractValidator<StartWorkOrderCommand>
{
    public StartWorkOrderValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("WorkOrderId is required.");
    }
}

public class StartWorkOrderHandler
{
    private readonly IWorkOrderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public StartWorkOrderHandler(IWorkOrderRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<WorkOrderDto> HandleAsync(StartWorkOrderCommand command, CancellationToken ct)
    {
        var order = await _repository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Work order with ID '{command.Id}' was not found.");

        order.StartExecution(command.Actor);
        await _repository.UpdateAsync(order, ct);
        await _unitOfWork.CommitAsync(ct);

        return WorkOrderDto.FromDomain(order);
    }
}

public record CompleteWorkOrderCommand(Guid Id, string ResolutionNotes, string Actor = "staff");

public class CompleteWorkOrderValidator : AbstractValidator<CompleteWorkOrderCommand>
{
    public CompleteWorkOrderValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("WorkOrderId is required.");
        RuleFor(x => x.ResolutionNotes).NotEmpty().MaximumLength(2000).WithMessage("ResolutionNotes are required.");
    }
}

public class CompleteWorkOrderHandler
{
    private readonly IWorkOrderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteWorkOrderHandler(IWorkOrderRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<WorkOrderDto> HandleAsync(CompleteWorkOrderCommand command, CancellationToken ct)
    {
        var order = await _repository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Work order with ID '{command.Id}' was not found.");

        order.Complete(command.ResolutionNotes, command.Actor);
        await _repository.UpdateAsync(order, ct);
        await _unitOfWork.CommitAsync(ct);

        return WorkOrderDto.FromDomain(order);
    }
}

public record ResetWorkOrderCommand(Guid Id, string Actor = "staff");

public class ResetWorkOrderHandler
{
    private readonly IWorkOrderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ResetWorkOrderHandler(IWorkOrderRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<WorkOrderDto> HandleAsync(ResetWorkOrderCommand command, CancellationToken ct)
    {
        var order = await _repository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Work order with ID '{command.Id}' was not found.");

        // Calling ResetToInitialState directly throws InvalidStateTransitionException
        // leaving the order unmodified, no timeline event added, and commit is never reached.
        order.ResetToInitialState();
        await _repository.UpdateAsync(order, ct);
        await _unitOfWork.CommitAsync(ct);

        return WorkOrderDto.FromDomain(order);
    }
}
