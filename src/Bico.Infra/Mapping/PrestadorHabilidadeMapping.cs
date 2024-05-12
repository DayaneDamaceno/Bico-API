using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bico.Infra.Mapping;

internal static class PrestadorHabilidadeMapping
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrestadorHabilidade>(entity =>
        {
            entity.ToTable("prestadores_habilidades");

            entity.Property(p => p.PrestadorId)
                  .HasColumnName("prestador_id")
                  .IsRequired();

            entity.Property(p => p.HabilidadeId)
                  .HasColumnName("habilidade_id")
                  .IsRequired();

            entity.HasKey(p => new { p.PrestadorId, p.HabilidadeId });
        });
    }
}
