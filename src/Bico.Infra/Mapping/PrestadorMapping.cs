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

            //entity.Property(a => a.Id)
            //      .HasColumnName("id");

            //entity.HasKey(a => a.Id);

            //entity.Property(a => a.Tipo)
            //      .HasColumnName("tipo");

            //entity
            //    .Property(a => a.Nome)
            //    .HasColumnName("nome")
            //    .IsRequired()
            //    .HasMaxLength(100);

            //entity.Property(a => a.Tamanho)
            //      .HasColumnName("tamanho");

            //entity
            //    .Property(a => a.DataReferencia)
            //    .HasColumnName("data-referencia")
            //    .IsRequired();

            //entity.Property(a => a.EnvioId)
            //      .HasColumnName("envio-id");

            //entity
            //    .HasOne(a => a.Envio)
            //    .WithMany(e => e.Arquivos)
            //    .HasForeignKey(a => a.EnvioId);

        });
    }
}

