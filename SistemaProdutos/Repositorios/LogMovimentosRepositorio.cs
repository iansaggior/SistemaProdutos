using Dapper;
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

        // ADIÇÃO -> ADI
        // REMOÇÃO -> REM
        // CRIAÇÃO -> CRIA
        public async Task<List<LogMovimentoModel>> UltimasMovimentacoes(string type)
        {
            try
            {
                IQueryable<LogMovimentoModel> query = _dbContext.Log_Movimentos;

                query = query.Where(x => x.TextoMovimento.Contains(type.Substring(0,3))).
                                Include(x => x.Produto).
                                OrderByDescending(x => x.MovId);

                var logMovimentos = await query.ToListAsync();

                return logMovimentos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Task<List<LogMovimentoModel>> UltimasMovimentacoes(DateTime dtInicio, DateTime dtFinal, string type)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LogMovimentoModel>> UltimasMovimentacoes(int id)
        {
            try
            {
                IQueryable<LogMovimentoModel> query = _dbContext.Log_Movimentos;

                query = query.Where(x => x.ProdutoId == id).
                                Include(x => x.Produto).
                                OrderByDescending(x => x.MovId);

                var logMovimentos = await query.ToListAsync();

                return logMovimentos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<LogMovimentoModel>> UltimasMovimentacoes()
        {
            try
            {
                IQueryable<LogMovimentoModel> query = _dbContext.Log_Movimentos;

                query = query.Include(x => x.Produto).
                    OrderByDescending(x => x.MovId);

                var logMovimentos = await query.ToListAsync();

                return logMovimentos;
            }
            catch (Exception)
            {
                throw;
            }
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
