using Microsoft.EntityFrameworkCore;
using SistemaProdutos.Data;
using SistemaProdutos.Models;
using SistemaProdutos.Repositorios.Interfaces;
using System.Data;

namespace SistemaProdutos.Repositorios
{
    public class LogMovimentosRepositorio : ILogMovimentosRepositorio
    {
        private readonly SistemaProdutosDBContext _dbContext;
        private readonly IDbConnection _dbConnection;

        public LogMovimentosRepositorio(SistemaProdutosDBContext sistemaProdutosDBContext, IDbConnection dbConnection)
        {
            _dbContext = sistemaProdutosDBContext;
            _dbConnection = dbConnection;
        }

        public async Task<List<LogMovimentoModel>> UltimasMovimentacoes(DateTime dtInicio, DateTime dtFinal)
        {
            try
            {
                IQueryable<LogMovimentoModel> query = _dbContext.Log_Movimentos;

                if (dtInicio == dtFinal)
                {
                    query = query.Where(x => x.DataMovimentacao.Date == dtInicio.Date);
                }
                else
                {
                    query = query.Where(x => x.DataMovimentacao.Date >= dtInicio.Date && x.DataMovimentacao.Date <= dtFinal.Date);
                }

                var logMovimentos = await query
                    .Include(x => x.Produto)
                    .OrderByDescending(x => x.DataMovimentacao)
                    .ToListAsync();

                return logMovimentos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<LogMovimentoModel>> UltimasMovimentacoes(DateTime dtInicio, DateTime dtFinal, int id)
        {
            try
            {
                IQueryable<LogMovimentoModel> query = _dbContext.Log_Movimentos;

                if (dtInicio == dtFinal)
                {
                    query = query.Where(x => x.DataMovimentacao.Date == dtInicio.Date && x.ProdutoId == id);
                }
                else
                {
                    query = query.Where(x => x.DataMovimentacao.Date >= dtInicio.Date && x.DataMovimentacao.Date <= dtFinal.Date && x.ProdutoId == id);
                }

                var logMovimentos = await query
                    .Include(x => x.Produto)
                    .OrderByDescending(x => x.DataMovimentacao)
                    .ToListAsync();

                return logMovimentos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
