using Microsoft.EntityFrameworkCore;
using SistemaProdutos.Data.Map;
using SistemaProdutos.Models;

namespace SistemaProdutos.Data
{
    public class SistemaProdutosDBContext : DbContext
    {
        public SistemaProdutosDBContext(DbContextOptions<SistemaProdutosDBContext> options ) : base( options )
        {
        }

        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<ProdutoAuditModel> Produtos_AUDIT { get; set; }
        public DbSet<LogMovimentoModel> Log_Movimentos { get; set; }
        public DbSet<MovimentacaoModel> Movimentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new ProdutoAuditMap());
            modelBuilder.ApplyConfiguration(new LogMovimentoMap());
            modelBuilder.ApplyConfiguration(new MovimentosMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
