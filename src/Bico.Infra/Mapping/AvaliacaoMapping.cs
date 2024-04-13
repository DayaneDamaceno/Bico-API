using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bico.Infra.Mapping;

internal static class AvaliacaoMapping
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Avaliacao>(entity =>
        {
            entity.ToTable("avaliacoes");

            entity.Property(avaliacao => avaliacao.Id)
                  .HasColumnName("id");

            entity.HasKey(avaliacao => avaliacao.Id);

            entity.Property(avaliacao => avaliacao.Conteudo)
                   .HasColumnName("conteudo");

            entity.Property(avaliacao => avaliacao.QuantidadeEstrelas)
                   .HasColumnName("quantidade_estrelas")
                   .IsRequired();

            entity
                .Property(avaliacao => avaliacao.PrestadorId)
                .HasColumnName("prestador_id")
                .IsRequired();

            entity
                .Property(avaliacao => avaliacao.ClienteId)
                .HasColumnName("cliente_id")
                .IsRequired();

            entity.HasOne(avaliacao => avaliacao.Cliente)
                   .WithMany()
                   .HasForeignKey(avaliacao => avaliacao.ClienteId);

            entity.HasOne(avaliacao => avaliacao.Prestador)
                   .WithMany(prestador => prestador.Avaliacoes)
                   .HasForeignKey(avaliacao => avaliacao.PrestadorId);
        });
    }
}
