using DAL;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class ContainerDependencies
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {

            services.AddScoped<ICRUDRepository<SolicitudServicio, int>, SolicitudServicioRepository>();

            services.AddDbContext<copystartdbContext>(config =>
            {
                config.UseNpgsql(connectionString);
            });

        }
    }
}
