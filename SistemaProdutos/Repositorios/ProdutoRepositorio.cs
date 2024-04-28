using Microsoft.EntityFrameworkCore;
using SistemaProdutos.Data;
using SistemaProdutos.Models;
using SistemaProdutos.Repositorios.Interfaces;

namespace SistemaProdutos.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly SistemaProdutosDBContext _dbContext;
        public ProdutoRepositorio(SistemaProdutosDBContext sistemaProdutosDBContext)
        {
            _dbContext = sistemaProdutosDBContext;
        }
        public async Task<ProdutoModel> BuscarProdutoPorId(int id)
        {
            ProdutoModel ProdutoPorId = await _dbContext.Produtos.FirstOrDefaultAsync(x => x.ProdutoId == id);

            if (ProdutoPorId == null)
                throw new Exception($"Produto com o ID '{id}' não foi encontrado no Banco de Dados");

            return ProdutoPorId;
        }

        public async Task<List<ProdutoModel>> BuscarTodosProdutos()
        {
            return await _dbContext.Produtos.ToListAsync();
        }
        public async Task<ProdutoModel> AdicionarProduto(ProdutoModel produto)
        {
            await _dbContext.Produtos.AddAsync(produto);
            await _dbContext.SaveChangesAsync();

            var prodAudt = new ProdutoAuditModel(produto, false);

            await _dbContext.Produtos_AUDIT.AddAsync(prodAudt);

            await _dbContext.SaveChangesAsync();

            return produto;
        }

        public async Task<ProdutoModel> AtualizarProdutoPorId(ProdutoModel produto, int id)
        {
            ProdutoModel ProdutoBuscaId = await BuscarProdutoPorId(id);

            var prodAudt = new ProdutoAuditModel(ProdutoBuscaId, true);

            await _dbContext.Produtos_AUDIT.AddAsync(prodAudt);

            await _dbContext.SaveChangesAsync();

            ProdutoBuscaId.Nome = produto.Nome;
            ProdutoBuscaId.Descricao = produto.Descricao;
            ProdutoBuscaId.Valor = produto.Valor;
            ProdutoBuscaId.Peso = produto.Peso;
            ProdutoBuscaId.Inativo = produto.Inativo;

            _dbContext.Produtos.Update(ProdutoBuscaId);

            await _dbContext.SaveChangesAsync();

            prodAudt = new ProdutoAuditModel(ProdutoBuscaId, true) {
                TypeAudit = "NEW"
            };

            await _dbContext.Produtos_AUDIT.AddAsync(prodAudt);

            await _dbContext.SaveChangesAsync();


            return ProdutoBuscaId;

        }

        public async Task<bool> InativarProduto(int id)
        {
            ProdutoModel ProdutoBuscaId = await BuscarProdutoPorId(id);
            ProdutoBuscaId.Inativo = !ProdutoBuscaId.Inativo;

            _dbContext.Produtos.Update(ProdutoBuscaId);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
