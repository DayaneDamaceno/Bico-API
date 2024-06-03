using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bico.Infra.Mapping;

internal  class FotoServicoMapping : IEntityTypeConfiguration<FotoServico>
{
    public void Configure(EntityTypeBuilder<FotoServico> builder)
    {
        builder.ToTable("foto_servico");

        builder.Property(p => p.Id)
              .HasColumnName("id");

        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Foto)
            .HasColumnName("foto")
            .IsRequired()
            .HasMaxLength(255);

        builder
          .Property(p => p.PrestadorId)
          .HasColumnName("prestador_id")
          .IsRequired();

        builder.HasOne(p => p.Prestador)
            .WithMany(p => p.Fotos)
            .HasForeignKey(p => p.PrestadorId);
    }
}

