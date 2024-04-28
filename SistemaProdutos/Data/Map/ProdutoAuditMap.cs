﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaProdutos.Models;

namespace SistemaProdutos.Data.Map
{
    public class ProdutoAuditMap : IEntityTypeConfiguration<ProdutoAuditModel>
    {
        public void Configure(EntityTypeBuilder<ProdutoAuditModel> builder)
        {
            builder.HasKey(x => x.AuditId);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Descricao).HasMaxLength(100);
            builder.Property(x => x.Peso).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Inativo);
            builder.Property(x => x.ProdutoId);

            builder.HasOne(x => x.Produto);
        }
    }
}