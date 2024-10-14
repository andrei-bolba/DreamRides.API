using System.Reflection;
using DreamRides.Database.Context;
using DreamRidesAPI.Extensions;
using Microsoft.EntityFrameworkCore;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        Assembly[] allCoreProjectsAssembly =
        [
            typeof(DreamRides.Service.DependencyInjection).Assembly
        ];
        
        builder.Services.AddApplicationServices(builder.Configuration, allCoreProjectsAssembly);
        builder.Services.AddIdentityServices(builder.Configuration);
        builder.Services.AddDbContext<DealershipContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DealershipDatabase")));


        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}