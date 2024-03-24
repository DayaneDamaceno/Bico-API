using Bico.Domain.Interfaces;
using Bico.Domain.Services;
using Bico.Infra.DBContext;
using Bico.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bico.Api.Configuration;

public static class InjectionService
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Services
        services.AddScoped<IPrestadorService, PrestadorService>();

        //Repositories
        services.AddScoped<IPrestadorRepository, PrestadorRepository>();
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();

        return services;
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BicoContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Default"));
        });

        return services;
    }

}
