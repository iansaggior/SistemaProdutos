using SistemaProdutos.Models;

namespace SistemaProdutos.Repositorios.Interfaces
{
    public interface IProdutoRepositorio
    {
        Task<List<ProdutoModel>> BuscarTodosProdutos();
        Task<ProdutoModel> BuscarProdutoPorId(int id);
        Task<ProdutoModel> AdicionarProduto(ProdutoModel produto);
        Task<ProdutoModel> AtualizarProdutoPorId(ProdutoModel produto, int id);
        Task<bool> InativarProduto(int id);
    }
}
