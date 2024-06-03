using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bico.Infra.Mapping;

internal class PrestadorMapping : IEntityTypeConfiguration<Prestador>
{
    public void Configure(EntityTypeBuilder<Prestador> builder)
    {
        builder.ToTable("prestadores");

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
            .Property(p => p.Sobre)
                .HasColumnName("sobre");

        builder
            .Property(p => p.RaioDeAlcance)
                .HasColumnName("raio_de_alcance")
                .IsRequired();


        builder.HasMany(p => p.Fotos)
                .WithOne(p => p.Prestador)
                .HasForeignKey(p => p.PrestadorId);
        builder
            .HasMany(p => p.Habilidades)
            .WithMany(h => h.Prestadores)
            .UsingEntity<Dictionary<string, object>>(
                        "prestadores_habilidades",
                        j => j.HasOne<Habilidade>()
                                .WithMany()
                                .HasForeignKey("habilidade_id"),
                        j => j.HasOne<Prestador>()
                                .WithMany()
                                .HasForeignKey("prestador_id"),
                        j =>
                        {
                            j.ToTable("prestadores_habilidades");
                            j.HasKey("prestador_id", "habilidade_id");
                        });

    }
}

