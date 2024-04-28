using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaProdutos.Models;

namespace SistemaProdutos.Data.Map
{
    public class ProdutoMap : IEntityTypeConfiguration<ProdutoModel>
    {
        public void Configure(EntityTypeBuilder<ProdutoModel> builder)
        {
            builder.HasKey(x => x.ProdutoId);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Descricao).HasMaxLength(100);
            builder.Property(x => x.Peso).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.Valor).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.Quantidade).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.Inativo).HasDefaultValue(false);
        }
    }
}
