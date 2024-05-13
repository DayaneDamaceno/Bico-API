using Bico.Infra.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bico.IntegrationTests.Database;

internal class DatabaseFixture : IDisposable
{
    public BicoContext Context { get; private set; }

    private IDbContextTransaction Transaction;

    public DatabaseFixture()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        Context = new DbConnection().GetBicoContext();
        IniciarTransactionComBaseLimpa().Wait();
    }

    private async Task IniciarTransactionComBaseLimpa()
    {
        Transaction = await Context.Database.BeginTransactionAsync();

        await Context.Database.ExecuteSqlRawAsync(@"
                    TRUNCATE TABLE
                        development.prestadores_habilidades,
                        development.habilidades,
                        development.categorias,
                        development.avaliacoes,
                        development.prestadores,
                        development.clientes,
                        development.usuarios
                    RESTART IDENTITY CASCADE;
                ");

        await Context.SaveChangesAsync();
    }

    public async void Dispose()
    {
        await Transaction.RollbackAsync();
        await Transaction.DisposeAsync();
        await Context.DisposeAsync();
    }
}
