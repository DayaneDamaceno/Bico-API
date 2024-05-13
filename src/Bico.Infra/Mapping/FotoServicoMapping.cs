using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bico.Infra.Mapping;

internal static class FotoServicoMapping
{

    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FotoServico>(entity =>
        {
            entity.ToTable("foto_servico");

            entity.Property(p => p.Id)
                  .HasColumnName("id");

            entity.HasKey(p => p.Id);

            entity
                .Property(p => p.Foto)
                .HasColumnName("foto")
                .IsRequired()
                .HasMaxLength(255);
            entity
              .Property(p => p.PrestadorId)
              .HasColumnName("prestador_id")
              .IsRequired();

            entity.HasOne(p => p.Prestador)
                .WithMany(p => p.Fotos)
                .HasForeignKey(p => p.PrestadorId);
          
        });
    }
}

