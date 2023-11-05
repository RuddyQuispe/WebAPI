using Domain.Interfaces;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure;

public static class DependencyInjection
{
    public static void AddInfrastructureBase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((options) =>
            options.UseSqlServer(configuration.GetConnectionString("ApplicationDbContext")));
        //// Add AutoMapper
        //services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
    }
}