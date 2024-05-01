using Microsoft.EntityFrameworkCore;
using SistemaProdutos.Data;
using SistemaProdutos.Models;
using SistemaProdutos.Repositorios.Interfaces;
//LEO - Adiciona a dependencia do Dapper (baixar pelo NuGet) e System.Data
using Dapper;
using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SistemaProdutos.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly SistemaProdutosDBContext _dbContext;

        //LEO - adiciona o IDbConnection
        private readonly IDbConnection _dbConnection;

        //LEO - add param no construtor
        public ProdutoRepositorio(SistemaProdutosDBContext sistemaProdutosDBContext, IDbConnection dbConnection)
        {
            _dbContext = sistemaProdutosDBContext;
            _dbConnection = dbConnection;
        }

        public async Task<List<ProdutoModel>> TesteQuerySemParametro()
        {
            //StringBuilder query = new();

            //query.Append($"  SELECT                                                 ");
            //query.Append($"      {nameof(ProdutoModel.ProdutoId)} as ProdutoId,    ");
            //query.Append($"      {nameof(ProdutoModel.Quantidade)} as Quantidade,   ");
            //query.Append($"      {nameof(ProdutoModel.Valor)} as Valor,        ");
            //query.Append($"      {nameof(ProdutoModel.Inativo)} as Inativo,      ");
            //query.Append($"      {nameof(ProdutoModel.Descricao)} as Descricao     ");
            //query.Append($"  FROM                                                   ");
            //query.Append($"      produtos                                      ");

            string query = $"SELECT * FROM Produtos ORDER BY {nameof(ProdutoModel.Nome)} ASC";

            //LEO - o nome do cabeçalho deve ser o mesmo que o nome do atributo do objeto, por isso o as nameof

            //var produtos = await _dbConnection.QueryAsync<ProdutoModel>(query.ToString());
            var produtos = await _dbConnection.QueryAsync<ProdutoModel>(query);

            return produtos.AsList();
        }

        public async Task<ProdutoModel> TesteQueryComParametro(int id)
        {
            //StringBuilder query = new();
            //query.Append("  SELECT          ");
            //query.Append("      *           ");
            //query.Append("  FROM            ");
            //query.Append("  Produtos            ");
            //query.Append("  WHERE           ");
            //query.Append($"      {nameof(ProdutoModel.ProdutoId)} = @id    ");

            string query = $"SELECT * FROM Produtos WHERE {nameof(ProdutoModel.ProdutoId)} = @id ";

            var parameters = new { id = id };

            var produto = await _dbConnection.QueryFirstOrDefaultAsync<ProdutoModel>(query, parameters);
            //var produto = await _dbConnection.QueryFirstAsync<ProdutoModel>(query.ToString(), parameters);

            if (produto == null)
                throw new Exception($"Produto com o ID '{id}' não foi encontrado no Banco de Dados");

            return produto;
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
                var produtos = await _dbContext.Produtos.ToListAsync();
                produtos.Sort((x, y) => string.Compare(x.Nome, y.Nome, StringComparison.OrdinalIgnoreCase));
                return produtos;
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
                    TextoMovimento = $"Criação do produto '{produto.ProdutoId}' com {produto.Quantidade} unidades"
                };
                await _dbContext.Log_Movimentos.AddAsync(logMovimentacao);
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
                await _dbContext.Log_Movimentos.AddAsync(logMovimentacao);
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

                if (ProdutoBuscaId.Quantidade == 0)
                    throw new Exception($"ESTOQUE INDISPONÍVEL!!");

                if (ProdutoBuscaId.Quantidade < Math.Abs(qtde))
                    throw new Exception($"Você está tentando remover uma quantidade superior em relação a quantidade disponível no estoque!!" +
                                        $"\nQuantidade disponível: {ProdutoBuscaId.Quantidade} unidade(s)." +
                                        $"\n Quantidade informada: {qtde} unidade(s).");

                ProdutoBuscaId.Quantidade -= Math.Abs(qtde);

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
                await _dbContext.Log_Movimentos.AddAsync(logMovimentacao);
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
