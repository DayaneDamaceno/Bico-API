
using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bico.Infra.Mapping;

internal class HabilidadeMapping : IEntityTypeConfiguration<Habilidade>
{
    public void Configure(EntityTypeBuilder<Habilidade> builder)
    {
        builder.ToTable("habilidades");

        builder.Property(habilidade => habilidade.Id)
              .HasColumnName("id");

        builder.HasKey(habilidade => habilidade.Id);

        builder
            .Property(habilidade => habilidade.Nome)
            .HasColumnName("nome")
            .IsRequired();

        builder
            .Property(habilidade => habilidade.CategoriaId)
            .HasColumnName("categoria_id")
            .IsRequired();

        builder.HasOne(h => h.Categoria)
               .WithMany(c => c.Habilidades)
               .HasForeignKey(h => h.CategoriaId);



    }

}
