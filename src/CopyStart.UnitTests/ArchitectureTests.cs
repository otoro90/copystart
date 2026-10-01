using System.Reflection;
using CopyStart.Application;
using CopyStart.Domain.Common;
using CopyStart.Domain.Enums;
using CopyStart.Infrastructure;
using Xunit;

namespace CopyStart.UnitTests;

public class ArchitectureTests
{
    private static readonly Assembly DomainAssembly = typeof(IAggregateRoot).Assembly;
    private static readonly Assembly ApplicationAssembly = typeof(IApplicationAssemblyMarker).Assembly;
    private static readonly Assembly InfrastructureAssembly = typeof(DependencyInjection).Assembly;
    private static readonly Assembly ApiAssembly = typeof(Program).Assembly;

    [Fact]
    public void Domain_MustNotReference_Application_Infrastructure_Or_Api()
    {
        var referencedAssemblies = DomainAssembly.GetReferencedAssemblies().Select(a => a.Name).ToList();

        Assert.DoesNotContain("CopyStart.Application", referencedAssemblies);
        Assert.DoesNotContain("CopyStart.Infrastructure", referencedAssemblies);
        Assert.DoesNotContain("CopyStart.Api", referencedAssemblies);
        Assert.DoesNotContain("Microsoft.EntityFrameworkCore", referencedAssemblies);
        Assert.DoesNotContain("WolverineFx", referencedAssemblies);
    }

    [Fact]
    public void Application_MustNotReference_Infrastructure_Or_Api()
    {
        var referencedAssemblies = ApplicationAssembly.GetReferencedAssemblies().Select(a => a.Name).ToList();

        Assert.DoesNotContain("CopyStart.Infrastructure", referencedAssemblies);
        Assert.DoesNotContain("CopyStart.Api", referencedAssemblies);
    }

    [Fact]
    public void Infrastructure_MustNotReference_Api()
    {
        var referencedAssemblies = InfrastructureAssembly.GetReferencedAssemblies().Select(a => a.Name).ToList();

        Assert.DoesNotContain("CopyStart.Api", referencedAssemblies);
    }

    [Fact]
    public void WorkRequest_And_WorkOrder_Statuses_MustBeDistinctEnums()
    {
        Assert.NotEqual(typeof(WorkRequestStatus), typeof(WorkOrderStatus));

        // Legacy states mapped correctly
        Assert.Equal("Por tramitar", WorkRequestStatus.PendingTriage.ToLegacyString());
        Assert.Equal("Asignada", WorkRequestStatus.Assigned.ToLegacyString());
        Assert.Equal("En servicio", WorkRequestStatus.InService.ToLegacyString());
        Assert.Equal("Servicios Finalizados", WorkRequestStatus.Completed.ToLegacyString());
        Assert.Equal("Cancelada", WorkRequestStatus.Cancelled.ToLegacyString());

        Assert.Equal("Por confirmar", WorkOrderStatus.PendingConfirmation.ToLegacyString());
        Assert.Equal("En ejecucion", WorkOrderStatus.InProgress.ToLegacyString());
        Assert.Equal("Finalizado", WorkOrderStatus.Completed.ToLegacyString());
    }
}
