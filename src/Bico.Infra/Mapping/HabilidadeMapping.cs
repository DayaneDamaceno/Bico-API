﻿using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bico.Infra.Mapping
{
    internal static class HabilidadeMapping
    {
        public static void Configure(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Habilidade>(entity =>
              {
                  entity.ToTable("habilidades");

                  entity.Property(habilidade => habilidade.Id)
                        .HasColumnName("id");

                  entity.HasKey(habilidade => habilidade.Id);

                  entity.Property(habilidade => habilidade.Nome)
                         .HasColumnName("nome")
                         .IsRequired();

                  entity.Property(habilidade => habilidade.CategoriaId)
                         .HasColumnName("categoria_id");

                  entity.HasOne(habilidade => habilidade.Categoria)
                          .WithMany(c => c.Habilidades)
                          .HasForeignKey(h => h.CategoriaId);
                  
              });
          
        }


    
    }
}
