using Dapper;
using Microsoft.EntityFrameworkCore;
using SistemaProdutos.Data;
using SistemaProdutos.Models;
using SistemaProdutos.Repositorios.Interfaces;
using System.ComponentModel.Design;
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

        //public async Task<List<LogMovimentoModel>> UltimasMovimentacoes()
        //{
        //    try
        //    {
        //        IQueryable<LogMovimentoModel> query = _dbContext.Log_Movimentos;

        //        query = query.Include(x => x.Produto).
        //            OrderByDescending(x => x.MovId);

        //        var logMovimentos = await query.ToListAsync();

        //        return logMovimentos;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public async Task<List<LogMovimentoModel>> UltimasMovimentacoes()
        {
            try
            {
                var logMovimentos = await _dbContext.Log_Movimentos.Include(x => x.Produto).
                                                                    OrderByDescending(x => x.MovId).
                                                                    ToListAsync();
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
                var logMovimentos = await UltimasMovimentacoes();

                if (dtInicio == dtFinal)
                    logMovimentos = logMovimentos.Where(x => x.DataMovimentacao.Date == dtInicio.Date).ToList();
                else
                    logMovimentos = logMovimentos.Where(x => x.DataMovimentacao.Date >= dtInicio.Date && x.DataMovimentacao.Date <= dtFinal.Date).ToList();

                return logMovimentos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // ADIÇÃO -> ADI
        // REMOÇÃO -> REM
        // CRIAÇÃO -> CRIA
        public async Task<List<LogMovimentoModel>> UltimasMovimentacoes(string type)
        {
            try
            {
                type = type.ToLower();
                var logMovimentos = await UltimasMovimentacoes();

                if (type.Substring(0, 3) == "cri" || type.Substring(0, 3) == "adi")
                    logMovimentos = logMovimentos.Where(x => x.TextoMovimento.Substring(0, 3).ToLower() == "cri" || x.TextoMovimento.Substring(0, 3).ToLower() == "adi").ToList();
                else
                    logMovimentos = logMovimentos.Where(x => x.TextoMovimento.Substring(0, 3).ToLower() == "rem").ToList();

                return logMovimentos;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<LogMovimentoModel>> UltimasMovimentacoes(DateTime dtInicio, DateTime dtFinal, string type)
        {
            type = type.ToLower();
            var logMovimentos = await UltimasMovimentacoes(dtInicio, dtFinal);

            if (type.Substring(0, 3) == "cri" || type.Substring(0, 3) == "adi")
                logMovimentos = logMovimentos.Where(x => x.TextoMovimento.Substring(0, 3).ToLower() == "cri" || x.TextoMovimento.Substring(0, 3).ToLower() == "adi").ToList();
            else
                logMovimentos = logMovimentos.Where(x => x.TextoMovimento.Substring(0, 3).ToLower() == "rem").ToList();

            return logMovimentos;
        }

        public async Task<List<LogMovimentoModel>> UltimasMovimentacoes(DateTime dtInicio, DateTime dtFinal, int id)
        {
            try
            {
                var logMovimentos = await UltimasMovimentacoes(dtInicio, dtFinal);
                
                return logMovimentos.Where(x => x.ProdutoId == id).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<LogMovimentoModel>> UltimasMovimentacoes(DateTime dtInicio, DateTime dtFinal, int id, string type)
        {
            var logMovimentos = await UltimasMovimentacoes(dtInicio, dtFinal, type);

            return logMovimentos.Where(x => x.ProdutoId == id).ToList();
        }

        public async Task<List<LogMovimentoModel>> UltimasMovimentacoes(int id)
        {
            try
            {
                List<LogMovimentoModel> logMovimentos = await UltimasMovimentacoes();

                return logMovimentos.Where(x => x.ProdutoId == id).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<LogMovimentoModel>> UltimasMovimentacoes(int id, string type)
        {
            try
            {
                List<LogMovimentoModel> logMovimentos = await UltimasMovimentacoes(type);

                return logMovimentos.Where(x => x.ProdutoId == id).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
