using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SistemaProdutos.Data;
using SistemaProdutos.Models;
using SistemaProdutos.Repositorios.Interfaces;
using System.ComponentModel.Design;
using System.Data;

namespace SistemaProdutos.Repositorios
{
    public class MovimentosRepositorio : IMovimentosRepositorio
    {
        private readonly SistemaProdutosDBContext _dbContext;
        private readonly IDbConnection _dbConnection;

        public MovimentosRepositorio(SistemaProdutosDBContext sistemaProdutosDBContext, IDbConnection dbConnection)
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
        public async Task<List<MovimentacaoModel>> UltimasMovimentacoes()
        {
            try
            {
                var movimentos = await _dbContext.Movimentos.Include(x => x.Produto).OrderByDescending(x => x.AuditId).ToListAsync();

                return movimentos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public async Task<List<MovimentacaoModel>> UltimasMovimentacoes()
        //{
        //    try
        //    {
        //        string query = $"SELECT * FROM View_Movimentacoes";

        //        var movimentos = await _dbConnection.QueryAsync<MovimentacaoModel>(query);

        //        return movimentos.AsList();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public async Task<List<MovimentacaoModel>> UltimasMovimentacoes(DateTime dtInicio, DateTime dtFinal)
        {
            try
            {
                var logMovimentos = await UltimasMovimentacoes();

                if (dtInicio == dtFinal)
                    logMovimentos = logMovimentos.Where(x => x.DataAlteracao.Date == dtInicio.Date).ToList();
                else
                    logMovimentos = logMovimentos.Where(x => x.DataAlteracao.Date >= dtInicio.Date && x.DataAlteracao.Date <= dtFinal.Date).ToList();

                return logMovimentos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<MovimentacaoModel>> UltimasMovimentacoes(string type)
        {
            try
            {
                if (type.IsNullOrEmpty())
                    throw new Exception("Informe o tipo de movimentação");

                type = type.ToUpper();
                var logMovimentos = await UltimasMovimentacoes();

                logMovimentos = (List<MovimentacaoModel>)logMovimentos.Where(x => x.TipoAlteracao == type).ToList();

                return logMovimentos;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<MovimentacaoModel>> UltimasMovimentacoes(DateTime dtInicio, DateTime dtFinal, string type)
        {
            type = type.ToLower();
            var logMovimentos = await UltimasMovimentacoes(dtInicio, dtFinal);

            if (type.IsNullOrEmpty())
                throw new Exception("Informe o tipo de movimentação");

            type = type.ToUpper();

            logMovimentos = (List<MovimentacaoModel>)logMovimentos.Where(x => x.TipoAlteracao == type).ToList();

            return logMovimentos;
        }

        public async Task<List<MovimentacaoModel>> UltimasMovimentacoes(DateTime dtInicio, DateTime dtFinal, int id)
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
        public async Task<List<MovimentacaoModel>> UltimasMovimentacoes(DateTime dtInicio, DateTime dtFinal, int id, string type)
        {
            var logMovimentos = await UltimasMovimentacoes(dtInicio, dtFinal, type);

            return logMovimentos.Where(x => x.ProdutoId == id).ToList();
        }

        public async Task<List<MovimentacaoModel>> UltimasMovimentacoes(int id)
        {
            try
            {
                List<MovimentacaoModel> logMovimentos = await UltimasMovimentacoes();

                return logMovimentos.Where(x => x.ProdutoId == id).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<MovimentacaoModel>> UltimasMovimentacoes(int id, string type)
        {
            try
            {
                List<MovimentacaoModel> logMovimentos = await UltimasMovimentacoes(type);

                return logMovimentos.Where(x => x.ProdutoId == id).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
