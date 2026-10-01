using CopyStart.Domain.Repositories;

namespace CopyStart.Application.WorkOrders;

public record GetWorkOrderByIdQuery(Guid Id);

public class GetWorkOrderByIdHandler
{
    private readonly IWorkOrderRepository _repository;

    public GetWorkOrderByIdHandler(IWorkOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<WorkOrderDto?> HandleAsync(GetWorkOrderByIdQuery query, CancellationToken ct)
    {
        var order = await _repository.GetByIdAsync(query.Id, ct);
        return order == null ? null : WorkOrderDto.FromDomain(order);
    }
}

public record ListWorkOrdersQuery();

public class ListWorkOrdersHandler
{
    private readonly IWorkOrderRepository _repository;

    public ListWorkOrdersHandler(IWorkOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<WorkOrderDto>> HandleAsync(ListWorkOrdersQuery query, CancellationToken ct)
    {
        var list = await _repository.ListAsync(ct);
        return list.Select(WorkOrderDto.FromDomain).ToList();
    }
}
