using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bico.Infra.Mapping;

internal class CategoriaMapping : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("categorias");

        builder.Property(categoria => categoria.Id)
              .HasColumnName("id");

        builder.HasKey(categoria => categoria.Id);

        builder.Property(categoria => categoria.Nome)
               .HasColumnName("nome")
               .IsRequired();

        builder.HasMany(c => c.Habilidades)
               .WithOne(h => h.Categoria)
               .HasForeignKey(h => h.CategoriaId);

    }
}
