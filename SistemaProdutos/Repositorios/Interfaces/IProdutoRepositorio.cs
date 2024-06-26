﻿using SistemaProdutos.Models;

namespace SistemaProdutos.Repositorios.Interfaces
{
    public interface IProdutoRepositorio
    {
        Task<List<ProdutoModel>> BuscarTodosProdutos();
        Task<ProdutoModel> BuscarProdutoPorId(int id);
        Task<List<ProdutoModel>> BuscarProdutoPorCoringa(string coriga);
        Task<List<ProdutoModel>> BuscarProdutoPorQtdeAsc();
        Task<List<ProdutoModel>> BuscarProdutoPorQtdeDesc();
        Task<List<ProdutoModel>> BuscarProdutoPorDataCadastro();
        Task<List<ProdutoModel>> BuscarProdutoPorDataCadastro(DateTime dtInicio, DateTime dtFinal);
        Task<ProdutoModel> AdicionarProduto(ProdutoModel produto);
        Task<ProdutoModel> AtualizarProdutoPorId(ProdutoModel produto, int id);
        Task<bool> InativarProduto(int id);

        Task<bool> AddQuantidadeProduto(int ProdutoId, decimal qtde);
        Task<bool> RemovQuantidadeProduto(int ProdutoId, decimal qtde);
    }
}
