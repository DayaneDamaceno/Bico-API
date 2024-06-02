using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using Bico.Domain.Interfaces;
using Bico.Domain.Services;
using Bico.Infra.DBContext;
using Bico.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Bico.Chat.Functions.Configuration;

public static class InjectionService
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        //Services
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAcordoService, AcordoService>();

        //Repositories
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IAvatarRepository, AvatarRepository>();
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
}
