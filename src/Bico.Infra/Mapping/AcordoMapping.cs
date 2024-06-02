
using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bico.Infra.Mapping;

internal class AcordoMapping : IEntityTypeConfiguration<Acordo>
{
    public void Configure(EntityTypeBuilder<Acordo> builder)
    {
        builder.ToTable("acordos");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
              .HasColumnName("id")
              .IsRequired();

        builder.Property(e => e.MensagemId)
              .HasColumnName("mensagem_id")
              .IsRequired();

        builder.Property(e => e.Descricao)
              .HasColumnName("descricao")
              .IsRequired();

        builder.Property(e => e.Valor)
              .HasColumnName("valor")
              .IsRequired()
              .HasColumnType("decimal");

        builder.Property(e => e.Resposta)
              .HasColumnName("resposta");

        builder.Property(e => e.RespondidoEm)
              .HasColumnName("respondido_em");

        builder.HasOne(e => e.Mensagem)
              .WithOne(m => m.Acordo)
              .HasForeignKey<Acordo>(e => e.MensagemId);
    }
}
