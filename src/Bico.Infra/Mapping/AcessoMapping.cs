using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bico.Infra.Mapping
{
    internal class AcessoMapping
    {
        public static void Configure(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Acesso>(entity =>
            {
                entity.ToTable("acessos");

                entity.Property(habilidade => habilidade.Id)
                      .HasColumnName("id");

                entity.HasKey(habilidade => habilidade.Id);

                entity
                    .Property(habilidade => habilidade.Email)
                    .HasColumnName("email")
                    .IsRequired();


                entity
                    .Property(habilidade => habilidade.Senha)
                    .HasColumnName("senha")
                    .IsRequired();

            });

        }
    }
}
