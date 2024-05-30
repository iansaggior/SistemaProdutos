using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaProdutos.Models;
using SistemaProdutos.Repositorios.Interfaces;
using System.Globalization;

namespace SistemaProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        public ProdutoController(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProdutoModel>>> BuscarTodosProdutos()
        {
            List<ProdutoModel> produtos = await _produtoRepositorio.BuscarTodosProdutos();
            return Ok(produtos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoModel>> BuscarPorId(int id)
        {
            ProdutoModel produto = await _produtoRepositorio.BuscarProdutoPorId(id);
            return Ok(produto);
        }
        [HttpGet("busca/{coringa}")]
        public async Task<ActionResult<List<ProdutoModel>>> BuscarProdutoPorCoringa(string coringa)
        {
            List<ProdutoModel> produtos = await _produtoRepositorio.BuscarProdutoPorCoringa(coringa);
            return Ok(produtos);
        }
        [HttpGet("buscaPorDataCadastro")]
        public async Task<ActionResult<List<ProdutoModel>>> BuscarTodosProdutosPorDataCadastro()
        {
            List<ProdutoModel> produtos = await _produtoRepositorio.BuscarProdutoPorDataCadastro();
            return Ok(produtos);
        }
        [HttpPost]
        public async Task<ActionResult<ProdutoModel>> CadastrarProduto([FromBody] ProdutoModel produtoModel)
        {
            ProdutoModel produto = await _produtoRepositorio.AdicionarProduto(produtoModel);
            return Ok(produto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoModel>> AtualizarProduto([FromBody] ProdutoModel produtoModel, int id)
        {
            produtoModel.ProdutoId = id;
            ProdutoModel produto = await _produtoRepositorio.AtualizarProdutoPorId(produtoModel, id);
            return Ok(produto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProdutoModel>> InativarProduto(int id)
        {
            bool inativar = await _produtoRepositorio.InativarProduto(id);
            return Ok(inativar);
        }

        [HttpPut("add/id={id}&qtde={qtde}")]
        public async Task<ActionResult<ProdutoModel>> AddQuantidadeProduto(int id, decimal qtde)
        {
            bool addQtde = await _produtoRepositorio.AddQuantidadeProduto(id, qtde);
            return Ok(addQtde);
        }

        [HttpPut("rem/id={id}&qtde={qtde}")]
        public async Task<ActionResult<ProdutoModel>> RemovQuantidadeProduto(int id, decimal qtde)
        {
            bool addQtde = await _produtoRepositorio.RemovQuantidadeProduto(id, qtde);
            return Ok(addQtde);
        }

    }
}
