namespace CopyStart.Domain.Enums;

public enum WorkOrderStatus
{
    PendingConfirmation = 1,
    InProgress = 2,
    Completed = 3
}

public static class WorkOrderStatusExtensions
{
    public static string ToLegacyString(this WorkOrderStatus status) => status switch
    {
        WorkOrderStatus.PendingConfirmation => "Por confirmar",
        WorkOrderStatus.InProgress => "En ejecucion",
        WorkOrderStatus.Completed => "Finalizado",
        _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
    };

    public static WorkOrderStatus FromLegacyString(string legacyStatus) => legacyStatus switch
    {
        "Por confirmar" => WorkOrderStatus.PendingConfirmation,
        "En ejecucion" => WorkOrderStatus.InProgress,
        "Finalizado" => WorkOrderStatus.Completed,
        _ => throw new ArgumentException($"Unknown legacy work order status: {legacyStatus}", nameof(legacyStatus))
    };
}
