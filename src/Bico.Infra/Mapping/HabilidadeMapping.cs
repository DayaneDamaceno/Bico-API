
using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bico.Infra.Mapping;

internal static class HabilidadeMapping
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Habilidade>(entity =>
        {
            entity.ToTable("habilidades");

            entity.Property(p => p.Id)
                  .HasColumnName("id");

            entity.HasKey(p => p.Id);

            entity
                .Property(p => p.Nome)
                .HasColumnName("nome")
                .IsRequired();

            entity
                .Property(p => p.CategoriaId)
                .HasColumnName("categoria_id")
                .IsRequired();

            entity.HasOne(h => h.Categoria)
                   .WithMany()
                   .HasForeignKey(h => h.CategoriaId);

            //entity
            //    .HasMany(h => h.Prestadores)
            //    .WithMany(p => p.Habilidades)
            //    .UsingEntity(j => j.ToTable("prestadores_habilidades").HasKey("prestador_id", "habilidade_id"));
        });
    }
}
