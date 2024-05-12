using Bico.Domain.Entities;
using Bico.Infra.Extensions;
using Bico.Infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Bico.Infra.DBContext;

public partial class BicoContext : DbContext
{
    public BicoContext() { }

    public BicoContext(DbContextOptions<BicoContext> options) : base(options) { }

    public virtual DbSet<Categoria> Categorias { get; set; } = null!;
    public virtual DbSet<Habilidade> Habilidades { get; set; } = null!;
    public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
    public virtual DbSet<Prestador> Prestadores { get; set; } = null!;
    public virtual DbSet<Cliente> Clientes { get; set; } = null!;
    public virtual DbSet<Avaliacao> Avaliacoes { get; set; } = null!;
    public virtual DbSet<PrestadorHabilidade> PrestadoresHabilidades { get; set; } = null!;
    public virtual DbSet<FotoServico> FotosServicos { get; set; } = null!;




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

        modelBuilder.HasDefaultSchema(environmentName.ToLower());

        PostGeoExtensions.AddStDWithin(modelBuilder);
        PostGeoExtensions.AddStDistance(modelBuilder);

        PrestadorHabilidadeMapping.Configure(modelBuilder);
        CategoriaMapping.Configure(modelBuilder);
        HabilidadeMapping.Configure(modelBuilder);
        UsuarioMapping.Configure(modelBuilder);
        PrestadorMapping.Configure(modelBuilder);
        ClienteMapping.Configure(modelBuilder);
        AvaliacaoMapping.Configure(modelBuilder);
        FotoServicoMapping.Configure(modelBuilder);


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}

