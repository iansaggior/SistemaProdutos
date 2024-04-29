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
            try
            {
                ProdutoModel ProdutoPorId = await _dbContext.Produtos.FirstOrDefaultAsync(x => x.ProdutoId == id);

                if (ProdutoPorId == null)
                    throw new Exception($"Produto com o ID '{id}' não foi encontrado no Banco de Dados");

                return ProdutoPorId;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ProdutoModel>> BuscarTodosProdutos()
        {
            try
            {
                return await _dbContext.Produtos.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<ProdutoModel> AdicionarProduto(ProdutoModel produto)
        {
            try
            {
                await _dbContext.Produtos.AddAsync(produto);
                await _dbContext.SaveChangesAsync();

                var prodAudt = new ProdutoAuditModel(produto, false);

                await _dbContext.Produtos_AUDIT.AddAsync(prodAudt);

                await _dbContext.SaveChangesAsync();

                var logMovimentacao = new LogMovimentoModel
                {
                    ProdutoId = produto.ProdutoId,
                    TextoMovimento = $"Criação do produto {produto.ProdutoId} com {produto.Quantidade} unidades"
                };
                await _dbContext.LogMovimentos.AddAsync(logMovimentacao);
                await _dbContext.SaveChangesAsync();

                return produto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProdutoModel> AtualizarProdutoPorId(ProdutoModel produto, int id)
        {
            try
            {
                ProdutoModel ProdutoBuscaId = await BuscarProdutoPorId(id);

                var prodAudt = new ProdutoAuditModel(ProdutoBuscaId, true);

                await _dbContext.Produtos_AUDIT.AddAsync(prodAudt);

                ProdutoBuscaId.Nome = produto.Nome;
                ProdutoBuscaId.Descricao = produto.Descricao;
                ProdutoBuscaId.Valor = produto.Valor;
                ProdutoBuscaId.Quantidade = produto.Quantidade;
                ProdutoBuscaId.Peso = produto.Peso;
                ProdutoBuscaId.Inativo = produto.Inativo;

                _dbContext.Produtos.Update(ProdutoBuscaId);

                prodAudt = new ProdutoAuditModel(ProdutoBuscaId, true)
                {
                    TypeAudit = "NEW"
                };

                await _dbContext.Produtos_AUDIT.AddAsync(prodAudt);

                await _dbContext.SaveChangesAsync();

                return ProdutoBuscaId;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> InativarProduto(int id)
        {
            try
            {
                ProdutoModel ProdutoBuscaId = await BuscarProdutoPorId(id);

                var prodAudt = new ProdutoAuditModel(ProdutoBuscaId, true);
                await _dbContext.Produtos_AUDIT.AddAsync(prodAudt);

                ProdutoBuscaId.Inativo = !ProdutoBuscaId.Inativo;

                _dbContext.Produtos.Update(ProdutoBuscaId);

                prodAudt = new ProdutoAuditModel(ProdutoBuscaId, true)
                {
                    TypeAudit = "NEW"
                };
                await _dbContext.Produtos_AUDIT.AddAsync(prodAudt);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> AddQuantidadeProduto(int ProdutoId, decimal qtde)
        {
            try
            {
                ProdutoModel ProdutoBuscaId = await BuscarProdutoPorId(ProdutoId);

                var prodAudt = new ProdutoAuditModel(ProdutoBuscaId, true);
                await _dbContext.Produtos_AUDIT.AddAsync(prodAudt);

                ProdutoBuscaId.Quantidade += Math.Abs(qtde);
                _dbContext.Produtos.Update(ProdutoBuscaId);

                prodAudt = new ProdutoAuditModel(ProdutoBuscaId, true)
                {
                    TypeAudit = "NEW"
                };
                await _dbContext.Produtos_AUDIT.AddAsync(prodAudt);

                var logMovimentacao = new LogMovimentoModel
                {
                    ProdutoId = ProdutoBuscaId.ProdutoId,
                    TextoMovimento = $"Adição de {qtde} unidade(s) do produto '{ProdutoBuscaId.ProdutoId}' no estoque. Total atualizado para {ProdutoBuscaId.Quantidade} unidade(s)"
                };
                await _dbContext.LogMovimentos.AddAsync(logMovimentacao);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> RemovQuantidadeProduto(int ProdutoId, decimal qtde)
        {
            try
            {
                ProdutoModel ProdutoBuscaId = await BuscarProdutoPorId(ProdutoId);

                var prodAudt = new ProdutoAuditModel(ProdutoBuscaId, true);
                await _dbContext.Produtos_AUDIT.AddAsync(prodAudt);

                decimal qtdeDisp = ProdutoBuscaId.Quantidade;
                decimal qtdeUpdate = (qtdeDisp - Math.Abs(qtde));

                if (qtdeUpdate < 0)
                    throw new Exception($"Você está tentando remover uma quantidade superior em relação a quantidade disponivel no estoque!! Quantidade disponivel: {qtdeDisp} unidade(s)");
                
                ProdutoBuscaId.Quantidade = qtdeUpdate;

                _dbContext.Produtos.Update(ProdutoBuscaId);

                prodAudt = new ProdutoAuditModel(ProdutoBuscaId, true)
                {
                    TypeAudit = "NEW"
                };
                await _dbContext.Produtos_AUDIT.AddAsync(prodAudt);

                var logMovimentacao = new LogMovimentoModel
                {
                    ProdutoId = ProdutoBuscaId.ProdutoId,
                    TextoMovimento = $"Remoção de {qtde} unidade(s) do produto '{ProdutoBuscaId.ProdutoId}' no estoque. Total atualizado para {ProdutoBuscaId.Quantidade} unidade(s)"
                };
                await _dbContext.LogMovimentos.AddAsync(logMovimentacao);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
