using CopyStart.Domain.Repositories;

namespace CopyStart.Application.WorkRequests;

public record GetWorkRequestByIdQuery(Guid Id);

public class GetWorkRequestByIdHandler
{
    private readonly IWorkRequestRepository _repository;

    public GetWorkRequestByIdHandler(IWorkRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<WorkRequestDto?> HandleAsync(GetWorkRequestByIdQuery query, CancellationToken ct)
    {
        var request = await _repository.GetByIdAsync(query.Id, ct);
        return request == null ? null : WorkRequestDto.FromDomain(request);
    }
}

public record ListWorkRequestsQuery();

public class ListWorkRequestsHandler
{
    private readonly IWorkRequestRepository _repository;

    public ListWorkRequestsHandler(IWorkRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<WorkRequestDto>> HandleAsync(ListWorkRequestsQuery query, CancellationToken ct)
    {
        var list = await _repository.ListAsync(ct);
        return list.Select(WorkRequestDto.FromDomain).ToList();
    }
}
