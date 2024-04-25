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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
