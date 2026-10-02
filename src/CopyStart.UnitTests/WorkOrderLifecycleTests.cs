using CopyStart.Domain.Entities;
using CopyStart.Domain.Enums;
using CopyStart.Domain.Exceptions;
using CopyStart.Infrastructure.Persistence;
using Xunit;

namespace CopyStart.UnitTests;

public class WorkOrderLifecycleTests
{
    [Fact]
    public void WorkOrder_Creation_SetsInitialState_And_RecordsTimelineEvent()
    {
        var requestId = Guid.NewGuid();
        var order = new WorkOrder(Guid.NewGuid(), requestId, "tech-123", "Printer roller jam", actor: "dispatcher");

        Assert.Equal(WorkOrderStatus.PendingConfirmation, order.Status);
        Assert.Equal("Por confirmar", order.Status.ToLegacyString());
        Assert.Equal(requestId, order.WorkRequestId);
        Assert.Equal("tech-123", order.TechnicianUserId);
        Assert.StartsWith("WO-", order.OrderNumber);
        Assert.Single(order.Timeline);

        var createdEvent = order.Timeline.First();
        Assert.Equal("WorkOrderCreated", createdEvent.EventType);
        Assert.Equal("dispatcher", createdEvent.Actor);
    }

    [Fact]
    public void WorkOrder_CanTransition_FromPendingConfirmation_ToInProgress()
    {
        var order = new WorkOrder(Guid.NewGuid(), Guid.NewGuid(), "tech-123", actor: "dispatcher");
        order.StartExecution("tech-123");

        Assert.Equal(WorkOrderStatus.InProgress, order.Status);
        Assert.Equal("En ejecucion", order.Status.ToLegacyString());
        Assert.NotNull(order.StartedAt);
        Assert.Equal(2, order.Timeline.Count);
        Assert.Equal("WorkOrderStarted", order.Timeline.Last().EventType);
    }

    [Fact]
    public void WorkOrder_CanTransition_FromInProgress_ToCompleted()
    {
        var order = new WorkOrder(Guid.NewGuid(), Guid.NewGuid(), "tech-123", actor: "dispatcher");
        order.StartExecution("tech-123");
        order.Complete("Replaced feed roller and cleaned sensor.", "tech-123");

        Assert.Equal(WorkOrderStatus.Completed, order.Status);
        Assert.Equal("Finalizado", order.Status.ToLegacyString());
        Assert.NotNull(order.CompletedAt);
        Assert.Equal("Replaced feed roller and cleaned sensor.", order.ResolutionNotes);
        Assert.Equal(3, order.Timeline.Count);
        Assert.Equal("WorkOrderCompleted", order.Timeline.Last().EventType);
    }

    [Fact]
    public void CompletedWorkOrder_CannotReturnToInitialState_FailsWithDomainError_NoStateChange_NoAudit_NoCommit()
    {
        // Arrange
        var uow = new InMemoryUnitOfWork();
        var order = new WorkOrder(Guid.NewGuid(), Guid.NewGuid(), "tech-123", actor: "dispatcher");
        order.StartExecution("tech-123");
        order.Complete("Fixed drive gear", "tech-123");

        var initialCommitCount = uow.CommitCount;
        var initialTimelineCount = order.Timeline.Count;
        Assert.Equal(3, initialTimelineCount);
        Assert.Equal(WorkOrderStatus.Completed, order.Status);

        // Act & Assert
        var ex = Assert.Throws<InvalidStateTransitionException>(() =>
        {
            try
            {
                order.ResetToInitialState();
                // Commit would only happen if transition succeeded
                uow.CommitAsync().GetAwaiter().GetResult();
            }
            catch
            {
                // Invariant: error rethrown, commit not executed
                throw;
            }
        });

        // Verify Invariants:
        // 1. Fails with domain error (InvalidStateTransitionException)
        Assert.Equal("Finalizado", ex.CurrentState);
        Assert.Equal("Por confirmar", ex.TargetState);

        // 2. Sin cambio de estado (state unchanged)
        Assert.Equal(WorkOrderStatus.Completed, order.Status);

        // 3. Sin auditoría (no audit/timeline added)
        Assert.Equal(initialTimelineCount, order.Timeline.Count);

        // 4. Sin Commit (unit of work not committed)
        Assert.Equal(initialCommitCount, uow.CommitCount);
    }

    [Fact]
    public void WorkOrder_CannotSkipExecution_DirectlyToCompleted()
    {
        var order = new WorkOrder(Guid.NewGuid(), Guid.NewGuid(), "tech-123", actor: "dispatcher");

        var ex = Assert.Throws<InvalidStateTransitionException>(() =>
        {
            order.Complete("Premature completion", "tech-123");
        });

        Assert.Equal(WorkOrderStatus.PendingConfirmation, order.Status);
        Assert.Single(order.Timeline);
    }
}
