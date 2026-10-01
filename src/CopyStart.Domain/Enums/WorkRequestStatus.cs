namespace CopyStart.Domain.Enums;

public enum WorkRequestStatus
{
    PendingTriage = 1,
    Assigned = 2,
    InService = 3,
    Completed = 4,
    Cancelled = 5
}

public static class WorkRequestStatusExtensions
{
    public static string ToLegacyString(this WorkRequestStatus status) => status switch
    {
        WorkRequestStatus.PendingTriage => "Por tramitar",
        WorkRequestStatus.Assigned => "Asignada",
        WorkRequestStatus.InService => "En servicio",
        WorkRequestStatus.Completed => "Servicios Finalizados",
        WorkRequestStatus.Cancelled => "Cancelada",
        _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
    };

    public static WorkRequestStatus FromLegacyString(string legacyStatus) => legacyStatus switch
    {
        "Por tramitar" => WorkRequestStatus.PendingTriage,
        "Asignada" => WorkRequestStatus.Assigned,
        "En servicio" => WorkRequestStatus.InService,
        "Servicios Finalizados" => WorkRequestStatus.Completed,
        "Cancelada" => WorkRequestStatus.Cancelled,
        _ => throw new ArgumentException($"Unknown legacy request status: {legacyStatus}", nameof(legacyStatus))
    };
}
