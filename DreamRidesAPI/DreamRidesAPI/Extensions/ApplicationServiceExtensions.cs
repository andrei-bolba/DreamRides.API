using System.Reflection;
using DreamRides.Communication.Interfaces;
using DreamRides.Communication.Repository;
using DreamRides.Database.Context;
using Microsoft.EntityFrameworkCore;
using ServiceCollectionExtensions = Microsoft.Extensions.DependencyInjection.ServiceCollectionExtensions;

namespace DreamRidesAPI.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config, Assembly[] coreProjectsAssemblies)
    {
        services.AddDbContext<DealershipContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("DealershipDatabase"));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        services.AddCors();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(coreProjectsAssemblies)
        );
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICarRepository, CarRepository>();

        return services;
    }
}