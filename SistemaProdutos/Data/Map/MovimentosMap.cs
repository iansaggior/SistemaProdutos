using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaProdutos.Models;

namespace SistemaProdutos.Data.Map
{
    public class MovimentosMap : IEntityTypeConfiguration<MovimentacaoModel>
    {
        public void Configure(EntityTypeBuilder<MovimentacaoModel> builder)
        {
            builder.ToView("View_Movimentacoes");
            builder.HasKey(x => x.ProdutoId);
            builder.HasOne(x => x.Produto).WithMany().HasForeignKey(x => x.ProdutoId);

        }
    }
}
