using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Bico.Infra.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Bico.IntegrationTests.Database;

internal class DbConnection
{
    private readonly DbContextOptions<BicoContext> _options;
    public DbConnection()
    {
        _options = new DbContextOptionsBuilder<BicoContext>()
            .UseNpgsql(GetConnectionStringFromKeyVault(), o => o.UseNetTopologySuite(geographyAsDefault: true))
            .Options;
    }

    public BicoContext GetBicoContext() => new(_options);

    private static string GetConnectionStringFromKeyVault()
    {
        string keyVaultUrl = "https://bico-vault.vault.azure.net/";
        var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
        KeyVaultSecret secret = client.GetSecret("DBStringConnection");
        return secret.Value;
    }
}
