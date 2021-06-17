using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VxTel.Application.Interfaces.Application;
using VxTel.Application.Services;
using VxTel.Domain.Interfaces.Repository;
using VxTel.Infrastructure.Context;
using VxTel.Infrastructure.Repositories;

namespace VxTel.Infrastructure.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void AddInfrastructureIoC(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("SqliteConnectionString");
            services.AddDbContext<VxTelDbContext>(options =>
                options.UseSqlite(connection));

            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IPriceRepository, PriceRepository>();

            services.AddScoped<IPlanApplication, PlanApplication>();
            services.AddScoped<IPriceApplication, PriceApplication>();

            services.AddScoped<IEstimateApplication, EstimateApplication>();
        }
    }
}
