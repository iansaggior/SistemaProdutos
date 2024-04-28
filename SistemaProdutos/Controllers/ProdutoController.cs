using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaProdutos.Models;
using SistemaProdutos.Repositorios.Interfaces;

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
        public async Task<ActionResult<List<ProdutoController>>> BuscarTodosProdutos()
        {
            List<ProdutoModel> produtos = await _produtoRepositorio.BuscarTodosProdutos();
            return Ok(produtos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoController>> BuscarPorId(int id)
        {
            ProdutoModel produto = await _produtoRepositorio.BuscarProdutoPorId(id);
            return Ok(produto);
        }
        [HttpPost]
        public async Task<ActionResult<ProdutoController>> CadastrarProduto([FromBody] ProdutoModel produtoModel)
        {
            ProdutoModel produto = await _produtoRepositorio.AdicionarProduto(produtoModel);
            return Ok(produto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoController>> AtualizarProduto([FromBody] ProdutoModel produtoModel, int id)
        {
            produtoModel.ProdutoId = id;
            ProdutoModel produto = await _produtoRepositorio.AtualizarProdutoPorId(produtoModel, id);
            return Ok(produto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProdutoController>> InativarProduto(int id)
        {
            bool inativar = await _produtoRepositorio.InativarProduto(id);
            return Ok(inativar);
        }

    }
}
