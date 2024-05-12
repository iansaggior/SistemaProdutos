using SistemaProdutos.Models;

namespace SistemaProdutos.Repositorios.Interfaces
{
    public interface IMovimentosRepositorio
    {
        Task<List<MovimentacaoModel>> UltimasMovimentacoes(DateTime dtInicio, DateTime dtFinal);
        Task<List<MovimentacaoModel>> UltimasMovimentacoes(DateTime dtInicio, DateTime dtFinal, int id);
        Task<List<MovimentacaoModel>> UltimasMovimentacoes(DateTime dtInicio, DateTime dtFinal, string type);
        Task<List<MovimentacaoModel>> UltimasMovimentacoes(DateTime dtInicio, DateTime dtFinal, int id, string type);
        Task<List<MovimentacaoModel>> UltimasMovimentacoes(string type);
        Task<List<MovimentacaoModel>> UltimasMovimentacoes(int id);
        Task<List<MovimentacaoModel>> UltimasMovimentacoes(int id, string type);
        Task<List<MovimentacaoModel>> UltimasMovimentacoes();

    }
}
