using Bico.Domain.Entities;
using Bico.Infra.Extensions;
using Bico.Infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Bico.Infra.DBContext;

public partial class BicoContext : DbContext
{

    public BicoContext(DbContextOptions<BicoContext> options) : base(options) { }

    public virtual DbSet<Categoria> Categorias { get; set; } = null!;
    public virtual DbSet<Habilidade> Habilidades { get; set; } = null!;
    public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
    public virtual DbSet<Prestador> Prestadores { get; set; } = null!;
    public virtual DbSet<Cliente> Clientes { get; set; } = null!;
    public virtual DbSet<Avaliacao> Avaliacoes { get; set; } = null!;
    public virtual DbSet<Mensagem> Mensagens { get; set; } = null!;



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

        modelBuilder.HasDefaultSchema(environmentName.ToLower());

        PostGeoExtensions.AddStDWithin(modelBuilder);
        PostGeoExtensions.AddStDistance(modelBuilder);


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BicoContext).Assembly);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}

