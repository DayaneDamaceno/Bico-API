using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bico.Infra.Mapping;

internal static class CategoriaMapping
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.ToTable("categorias");

            entity.Property(categoria => categoria.Id)
                  .HasColumnName("id");

            entity.HasKey(categoria => categoria.Id);

            entity.Property(categoria => categoria.Nome)
                   .HasColumnName("nome")
                   .IsRequired();

            entity.HasMany(c => c.Habilidades)
                   .WithOne(h => h.Categoria)
                   .HasForeignKey(h => h.CategoriaId);

        });
    }
}
