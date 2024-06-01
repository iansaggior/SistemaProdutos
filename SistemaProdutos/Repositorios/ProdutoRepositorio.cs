using Microsoft.EntityFrameworkCore;
using SistemaProdutos.Data;
using SistemaProdutos.Models;
using SistemaProdutos.Repositorios.Interfaces;
//LEO - Adiciona a dependencia do Dapper (baixar pelo NuGet) e System.Data
using Dapper;
using System.Data;

namespace SistemaProdutos.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly SistemaProdutosDBContext _dbContext;
        private readonly IDbConnection _dbConnection;

        public ProdutoRepositorio(SistemaProdutosDBContext sistemaProdutosDBContext, IDbConnection dbConnection)
        {
            _dbContext = sistemaProdutosDBContext;
            _dbConnection = dbConnection;
        }

        public async Task<List<ProdutoModel>> BuscarTodosProdutos()
        {
            try
            {
                string query = $"SELECT * FROM Produtos ORDER BY {nameof(ProdutoModel.Nome)} ASC";

                var produtos = await _dbConnection.QueryAsync<ProdutoModel>(query);

                return produtos.AsList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProdutoModel> BuscarProdutoPorId(int id)
        {
            try
            {
                string query = $"SELECT * FROM Produtos WHERE {nameof(ProdutoModel.ProdutoId)} = @id ";

                var parameters = new { id = id };

                var produto = await _dbConnection.QueryFirstOrDefaultAsync<ProdutoModel>(query, parameters);

                if (produto == null)
                    throw new Exception($"Produto com o ID '{id}' não foi encontrado no Banco de Dados");

                return produto;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //public async Task<ProdutoModel> BuscarProdutoPorId(int id)
        //{
        //    try
        //    {
        //        ProdutoModel ProdutoPorId = await _dbContext.Produtos.FirstOrDefaultAsync(x => x.ProdutoId == id);

        //        if (ProdutoPorId == null)
        //            throw new Exception($"Produto com o ID '{id}' não foi encontrado no Banco de Dados");

        //        return ProdutoPorId;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public async Task<List<ProdutoModel>> BuscarTodosProdutos()
        //{
        //    try
        //    {
        //        var produtos = await _dbContext.Produtos.ToListAsync();
        //        produtos.Sort((x, y) => string.Compare(x.Nome, y.Nome, StringComparison.OrdinalIgnoreCase));
        //        return produtos;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        // montando a query na mão
        //public async Task<List<ProdutoModel>> BuscarProdutoPorCoringa(string coringa)
        //{
        //    try
        //    {
        //        coringa.ToLower();

        //        // objetivo dessa query, é trazer todos os resultados possíveis não levando em consideração os espaços, seja no cadastro do produto, ou na busca do usuario(coriga)
        //        string query = @$"SELECT * FROM produtos 
        //                        WHERE REPLACE(TRIM(CONCAT(TRIM({nameof(ProdutoModel.ProdutoId)}), TRIM({nameof(ProdutoModel.Nome)}), TRIM({nameof(ProdutoModel.Descricao)}))), ' ', '')
        //                        LIKE REPLACE(TRIM('%{coringa}%'), ' ', '') ";

        //        var produtos = await _dbConnection.QueryAsync<ProdutoModel>(query);

        //        if (produtos == null)
        //            throw new Exception($"Nenhum produto com a palavra chave '{coringa}' não foi encontrado no Banco de Dados");

        //        return produtos.AsList();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        // usando o entity framework ao nosso favor
        public async Task<List<ProdutoModel>> BuscarProdutoPorCoringa(string coringa)
        {
            try
            {
                var produtos = await _dbContext.Produtos
                    .Where(p =>
                        (p.ProdutoId.ToString().Trim() + p.Nome.Trim() + p.Descricao.Trim()).Replace(" ", "").ToLower()
                            .Contains(coringa.Trim().Replace(" ", "").ToLower())
                    )
                    .ToListAsync();

                if (produtos.Count == 0)
                    throw new Exception($"Nenhum produto com a palavra chave '{coringa}' foi encontrado no Banco de Dados");

                return produtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProdutoModel>> BuscarProdutoPorQtdeAsc()
        {
            try
            {
                var produtos = await BuscarTodosProdutos();
                produtos = produtos.OrderBy(x => x.Quantidade).ToList();
                return produtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ProdutoModel>> BuscarProdutoPorQtdeDesc()
        {
            try
            {
                var produtos = await BuscarTodosProdutos();
                produtos = produtos.OrderByDescending(x => x.Quantidade).ToList();
                return produtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProdutoModel>> BuscarProdutoPorDataCadastro()
        {
            try
            {
                var produtos = await BuscarTodosProdutos();
                produtos = produtos.OrderByDescending(x => x.DataCadastro).ToList();
                return produtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProdutoModel>> BuscarProdutoPorDataCadastro(DateTime dtInicio, DateTime dtFinal)
        {
            try
            {
                var produtos = await BuscarProdutoPorDataCadastro();

                if (dtInicio == dtFinal)
                    produtos = produtos.Where(x => x.DataCadastro.Date == dtInicio.Date).ToList();
                else
                    produtos = produtos.Where(x => x.DataCadastro.Date >= dtInicio.Date && x.DataCadastro.Date <= dtFinal.Date).ToList();

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
                if (produto.Inativo == null)
                    produto.Inativo = false;

                await _dbContext.Produtos.AddAsync(produto);
                await _dbContext.SaveChangesAsync();

                var prodAudt = new ProdutoAuditModel(produto, false);

                await _dbContext.Produtos_AUDIT.AddAsync(prodAudt);

                await _dbContext.SaveChangesAsync();

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
