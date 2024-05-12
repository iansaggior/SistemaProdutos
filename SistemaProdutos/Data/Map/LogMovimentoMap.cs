//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using SistemaProdutos.Models;

//namespace SistemaProdutos.Data.Map
//{
//    public class LogMovimentoMap : IEntityTypeConfiguration<LogMovimentoModel>
//    {
//        public void Configure(EntityTypeBuilder<LogMovimentoModel> builder)
//        {
//            builder.HasKey(x => x.MovId);
//            builder.Property(x => x.TextoMovimento).HasMaxLength(100);
//            builder.Property(x => x.ProdutoId).IsRequired();
//            builder.Property(x => x.DataMovimentacao).IsRequired().HasColumnType("TIMESTAMP").HasDefaultValueSql("current_timestamp");

//            builder.HasOne(x => x.Produto);

//        }
//    }
//}
