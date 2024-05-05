using SistemaProdutos.Models;

namespace SistemaProdutos.Repositorios.Interfaces
{
    public interface ILogMovimentosRepositorio
    {
        Task<List<LogMovimentoModel>> UltimasMovimentacoes(DateTime dtInicio, DateTime dtFinal);
        Task<List<LogMovimentoModel>> UltimasMovimentacoes(DateTime dtInicio, DateTime dtFinal, int id);
        Task<List<LogMovimentoModel>> UltimasMovimentacoes(string type);
        Task<List<LogMovimentoModel>> UltimasMovimentacoes();

    }
}
