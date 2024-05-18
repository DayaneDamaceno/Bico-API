using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bico.Infra.Mapping;

internal class AvaliacaoMapping : IEntityTypeConfiguration<Avaliacao>
{
    public void Configure(EntityTypeBuilder<Avaliacao> builder)
    {
        builder.ToTable("avaliacoes");

        builder.Property(avaliacao => avaliacao.Id)
              .HasColumnName("id");

        builder.HasKey(avaliacao => avaliacao.Id);

        builder.Property(avaliacao => avaliacao.Conteudo)
               .HasColumnName("conteudo");

        builder.Property(avaliacao => avaliacao.QuantidadeEstrelas)
               .HasColumnName("quantidade_estrelas")
               .IsRequired();

        builder
            .Property(avaliacao => avaliacao.PrestadorId)
            .HasColumnName("prestador_id")
            .IsRequired();

        builder
            .Property(avaliacao => avaliacao.ClienteId)
            .HasColumnName("cliente_id")
            .IsRequired();

        builder.HasOne(avaliacao => avaliacao.Cliente)
               .WithMany()
               .HasForeignKey(avaliacao => avaliacao.ClienteId);

        builder.HasOne(avaliacao => avaliacao.Prestador)
               .WithMany(prestador => prestador.Avaliacoes)
               .HasForeignKey(avaliacao => avaliacao.PrestadorId);
    }
}
