using DreamRides.Communication.Interfaces;
using DreamRides.Communication.Repository;
using DreamRides.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace DreamRidesAPI.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<DealershipContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("DealershipDatabase"));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        services.AddCors();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}