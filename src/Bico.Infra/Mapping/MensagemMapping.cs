using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bico.Infra.Mapping;

internal class MensagemMapping : IEntityTypeConfiguration<Mensagem>
{
    public void Configure(EntityTypeBuilder<Mensagem> builder)
    {
        builder.ToTable("mensagens");

        builder.Property(m => m.Id)
                .IsRequired()
                .HasColumnName("id");

        builder.HasKey(m => m.Id);


        builder.Property(m => m.RemetenteId)
            .HasColumnName("remetente_id");

        builder.Property(m => m.DestinatarioId)
            .HasColumnName("destinatario_id");

        builder
            .Property(m => m.Conteudo)
            .HasColumnName("conteudo")
            .IsRequired();

        builder
           .Property(m => m.Tipo)
           .HasColumnName("tipo")
           .IsRequired();

        builder
            .Property(m => m.EnviadoEm)
            .HasColumnName("enviado_em")
            .IsRequired();

        builder
            .Property(m => m.MensagemLida)
            .HasColumnName("mensagem_lida")
            .HasDefaultValue(false);

        builder.HasOne(m => m.Remetente)
            .WithMany()
            .HasForeignKey(m => m.RemetenteId);

        builder.HasOne(m => m.Destinatario)
            .WithMany()
            .HasForeignKey(m => m.DestinatarioId);
    }
}
