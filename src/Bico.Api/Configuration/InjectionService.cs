using Bico.Domain.Interfaces;
using Bico.Domain.Services;
using Bico.Infra.DBContext;
using Bico.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Bico.Api.Configuration;

public static class InjectionService
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        NpgsqlConnection.GlobalTypeMapper.UseNetTopologySuite();
        //Services
        services.AddScoped<IPrestadorService, PrestadorService>();

        //Repositories
        services.AddScoped<IPrestadorRepository, PrestadorRepository>();
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();

        return services;
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("Default"));
        dataSourceBuilder.UseNetTopologySuite();
        var dataSource = dataSourceBuilder.Build();

        services.AddDbContext<BicoContext>(options =>
        {
            options.UseNpgsql(dataSource, o => o.UseNetTopologySuite(geographyAsDefault: true));
        });

        return services;
    }

}
