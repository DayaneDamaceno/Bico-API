using Bico.Domain.Entities;
using Bico.Infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Bico.Infra.DBContext;

public partial class BicoContext : DbContext
{
    public BicoContext() { }

    public BicoContext(DbContextOptions<BicoContext> options) : base(options) { }

    public virtual DbSet<Categoria> Categorias { get; set; } = null!;
    public virtual DbSet<Habilidade> Habilidades { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("development");
        CategoriaMapping.Configure(modelBuilder);
        HabilidadeMapping.Configure(modelBuilder);


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}

