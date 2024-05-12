using Azure.Storage.Blobs;
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
        //Services
        services.AddScoped<IPrestadorService, PrestadorService>();
        services.AddScoped<IAuthService, AuthService>();

        //Repositories
        services.AddScoped<IPrestadorRepository, PrestadorRepository>();
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IAvatarRepository, AvatarRepository>();
        services.AddScoped<IHabilidadeRepository, HabilidadeRepository>();

        return services;
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration["DBStringConnection"]);
        dataSourceBuilder.UseNetTopologySuite();
        var dataSource = dataSourceBuilder.Build();

        services.AddDbContext<BicoContext>(options =>
        {
            options.UseNpgsql(dataSource, o => o.UseNetTopologySuite(geographyAsDefault: true));
        });

        return services;
    }

    public static IServiceCollection AddBlobClient(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["BlobStorageStringConnection"];
        services.AddScoped(_ => {
            var blobServiceClient = new BlobServiceClient(connectionString);
            return blobServiceClient;
        });

        return services;
    }
}
