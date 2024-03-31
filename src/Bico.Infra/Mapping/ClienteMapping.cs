﻿using Bico.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bico.Infra.Mapping;

internal static class ClienteMapping
{

    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("clientes");

            entity.Property(p => p.Id)
                  .HasColumnName("id");

            entity.HasKey(p => p.Id);

            entity
                .Property(p => p.Nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasMaxLength(255);

            entity
                .Property(p => p.AvatarUrl)
                .HasColumnName("avatar_url")
                .IsRequired()
                .HasMaxLength(255);

            entity
                .Property(p => p.Localizacao)
                .HasColumnType("geography (point)")
                .HasColumnName("localizacao");
        });
    }
}
