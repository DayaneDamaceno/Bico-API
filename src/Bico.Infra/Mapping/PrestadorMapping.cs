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

            entity
                .Property(p => p.Nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasMaxLength(255);

            entity
                .Property(p => p.AvatarFileName)
                .HasColumnName("avatar_filename")
                .IsRequired()
                .HasMaxLength(255);

            entity
                .Property(p => p.RaioDeAlcance)
                .HasColumnName("raio_de_alcance")
                .IsRequired();
           
        });
    }
}

