using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bico.Infra.Mapping;

internal class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");

        builder.Property(p => p.Id)
                  .HasColumnName("id");

        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Nome)
            .HasColumnName("nome")
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(p => p.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(255);

        builder
            .Property(p => p.Senha)
                .HasColumnName("senha")
                .IsRequired()
                .HasMaxLength(1000);
        builder
          .Property(p => p.Cpf)
          .HasColumnName("cpf")
          .IsRequired()
          .HasMaxLength(100);

        builder
            .Property(p => p.AvatarFileName)
                .HasColumnName("avatar_filename")
                .IsRequired()
                .HasMaxLength(255);

        builder
            .Property(p => p.Localizacao)
            .HasColumnType("geography (point)")
            .HasColumnName("localizacao");
    }
}

