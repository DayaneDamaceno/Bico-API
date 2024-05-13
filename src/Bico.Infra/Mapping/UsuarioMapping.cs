using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bico.Infra.Mapping;

internal static class UsuarioMapping
{

    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("usuarios");

            entity.Property(p => p.Id)
                  .HasColumnName("id");

            entity.HasKey(p => p.Id);

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
                .Property(p => p.Localizacao)
                .HasColumnType("geography (point)")
                .HasColumnName("localizacao");
        });
    }
}

