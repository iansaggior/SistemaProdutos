using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaProdutos.Models;
using SistemaProdutos.Repositorios.Interfaces;

namespace SistemaProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogMovimentosController : ControllerBase
    {
        private readonly ILogMovimentosRepositorio _logMovimentosRepositorio;
        public LogMovimentosController(ILogMovimentosRepositorio logMovimentosRepositorio)
        {
            _logMovimentosRepositorio = logMovimentosRepositorio;
        }


        [Route("logMovimentos/{dataInicio:datetime}/{dataFinal:datetime}")]
        [HttpGet]
        public async Task<ActionResult<List<ProdutoController>>> UltimasMovimentacoes(DateTime dataInicial, DateTime dataFinal)
        {
            List<LogMovimentoModel> logMovimentos = await _logMovimentosRepositorio.UltimasMovimentacoes(dataInicial, dataFinal);
            return Ok(logMovimentos);
        }

        [Route("logMovimentos/{dataInicio:datetime}/{dataFinal:datetime}/id={id}")]
        [HttpGet]
        public async Task<ActionResult<List<ProdutoController>>> UltimasMovimentacoesPorProduto(DateTime dataInicial, DateTime dataFinal, int id)
        {
            List<LogMovimentoModel> logMovimentos = await _logMovimentosRepositorio.UltimasMovimentacoes(dataInicial, dataFinal, id);
            return Ok(logMovimentos);
        }
    }
}

