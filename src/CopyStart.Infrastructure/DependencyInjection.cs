using CopyStart.Domain.Common;
using CopyStart.Domain.Repositories;
using CopyStart.Infrastructure.Persistence;
using CopyStart.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CopyStart.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Host=localhost;Database=copystart_dev;Username=postgres;Password=postgres";

        services.AddDbContext<CopyStartDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        // Current operational persistence uses in-memory implementations while EF persistence mapping is frozen
        services.AddSingleton<InMemoryUnitOfWork>();
        services.AddSingleton<IUnitOfWork>(sp => sp.GetRequiredService<InMemoryUnitOfWork>());
        services.AddSingleton<IWorkRequestRepository, InMemoryWorkRequestRepository>();
        services.AddSingleton<IWorkOrderRepository, InMemoryWorkOrderRepository>();
        services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>();

        return services;
    }
}
