using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bico.Infra.Mapping;

internal static class PrestadorMapping
{

    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prestador>(entity =>
        {
            entity.ToTable("prestadores");

            entity.Property(p => p.Id)
                  .HasColumnName("id");

            entity.HasKey(p => p.Id);

            entity
                .Property(p => p.Nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasMaxLength(255);

            entity
                .Property(p => p.AvatarUrl)
                .HasColumnName("avatar_url")
                .IsRequired()
                .HasMaxLength(255);

            entity
                .Property(p => p.RaioDeAlcance)
                .HasColumnName("raio_de_alcance")
                .IsRequired();

            entity
                .Property(p => p.Localizacao)
                .HasColumnType("geography (point)")
                .HasColumnName("localizacao");

            entity
                .HasMany(p => p.Habilidades)
                .WithMany(h => h.Prestadores)
                .UsingEntity<Dictionary<string, object>>( 
                            "prestadores_habilidades", 
                            j => j.HasOne<Habilidade>()
                                 .WithMany()
                                 .HasForeignKey("habilidade_id"),
                            j => j.HasOne<Prestador>() 
                                 .WithMany()
                                 .HasForeignKey("prestador_id"),
                            j =>
                            {
                                j.ToTable("prestadores_habilidades", "dev"); 
                                j.HasKey("prestador_id", "habilidade_id");
                            });
                });
    }
}

