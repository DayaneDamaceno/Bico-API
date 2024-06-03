using Azure.Messaging.ServiceBus;
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
        services.AddScoped<IAuthenticateService,AuthenticateService>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IAcordoService, AcordoService>();

        //Repositories
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IAvatarRepository, AvatarRepository>();
        services.AddScoped<IHabilidadeRepository, HabilidadeRepository>();
        services.AddScoped<IPrestadorRepository, PrestadorRepository>();

        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IAcordoRepository, AcordoRepository>();

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

    public static IServiceCollection AddServiceBusClient(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ServiceBusConnectionString"];
        services.AddScoped(_ => {
            var serviceBusClient = new ServiceBusClient(connectionString);
            return serviceBusClient;
        });

        return services;
    }
}
