using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bico.Infra.Mapping;

internal class ClienteMapping : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("clientes");

        builder.Property(p => p.Id)
                  .HasColumnName("id");

        builder
            .Property(p => p.Nome)
            .HasColumnName("nome")
            .IsRequired()
            .HasMaxLength(255);

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

