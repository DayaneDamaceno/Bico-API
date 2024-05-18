using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Bico.Chat.Functions.Configuration;
using Microsoft.Extensions.Azure;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration((context, config) =>
    {
        var builtConfig = config.Build();
        var keyVaultEndpoint = builtConfig["AzureKeyVaultEndpoint"];
        if (!string.IsNullOrEmpty(keyVaultEndpoint))
        {
            config.AddAzureKeyVault(new Uri(keyVaultEndpoint), new DefaultAzureCredential());
        }
    })
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddServiceBusClient(context.Configuration["ServiceBusConnectionString"]);
            clientBuilder.AddBlobServiceClient(context.Configuration["BlobStorageStringConnection"]);
        });
        services.AddDbContext(context.Configuration)
                .RegisterServices();

    })
    .Build();

host.Run();


