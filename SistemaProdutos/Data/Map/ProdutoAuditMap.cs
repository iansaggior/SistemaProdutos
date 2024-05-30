using Microsoft.EntityFrameworkCore;
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
            builder.Property(x => x.Peso).IsRequired().HasDefaultValue(0).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Valor).IsRequired().HasDefaultValue(0).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Quantidade).IsRequired().HasDefaultValue(0).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Inativo);
            builder.Property(x => x.DataAlteracao).HasColumnType("TIMESTAMP").HasDefaultValueSql("current_timestamp");
            builder.Property(x => x.DataCadastro).HasColumnType("TIMESTAMP");
            builder.Property(x => x.TypeAudit).HasMaxLength(3);
            //builder.Property(x => x.DataAlteracao).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.ProdutoId).IsRequired();

            builder.HasOne(x => x.Produto);
        }
    }
}
