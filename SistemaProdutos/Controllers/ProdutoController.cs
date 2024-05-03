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
        //[HttpGet("teste")]
        //public async Task<ActionResult<List<ProdutoController>>> TesteQuerySemParametro()
        //{
        //    List<ProdutoModel> produtos = await _produtoRepositorio.TesteQuerySemParametro();
        //    return Ok(produtos);
        //}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoController>> BuscarPorId(int id)
        {
            ProdutoModel produto = await _produtoRepositorio.BuscarProdutoPorId(id);
            return Ok(produto);
        }
        //[HttpGet("teste/{id}")]
        //public async Task<ActionResult<ProdutoController>> TesteQueryComParametro(int id)
        //{
        //    ProdutoModel produto = await _produtoRepositorio.TesteQueryComParametro(id);
        //    return Ok(produto);
        //}
        [HttpGet("busca/{coringa}")]
        public async Task<ActionResult<List<ProdutoController>>> BuscarProdutoPorCoringa(string coringa)
        {
            List<ProdutoModel> produtos = await _produtoRepositorio.BuscarProdutoPorCoringa(coringa);
            return Ok(produtos);
        }
        [Route("logMovimentos/{dataInicio:datetime}/{dataFinal:datetime}")]
        public async Task<ActionResult<List<ProdutoController>>> UltimasMovimentacoes(DateTime dataInicial, DateTime dataFinal)
        {
            List<LogMovimentoModel> produtos = await _produtoRepositorio.UltimasMovimentacoes(dataInicial, dataFinal);
            return Ok(produtos);
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

        [HttpPut("add/id={id}&qtde={qtde}")]
        public async Task<ActionResult<ProdutoController>> AddQuantidadeProduto(int id, decimal qtde)
        {
            bool addQtde = await _produtoRepositorio.AddQuantidadeProduto(id, qtde);
            return Ok(addQtde);
        }

        [HttpPut("rem/id={id}&qtde={qtde}")]
        public async Task<ActionResult<ProdutoController>> RemovQuantidadeProduto(int id, decimal qtde)
        {
            bool addQtde = await _produtoRepositorio.RemovQuantidadeProduto(id, qtde);
            return Ok(addQtde);
        }

    }
}
