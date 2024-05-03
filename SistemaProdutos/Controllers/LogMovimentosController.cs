using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaProdutos.Models;
using SistemaProdutos.Repositorios.Interfaces;
using System.Globalization;

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


        [Route("{dataInicio:datetime}/{dataFinal:datetime}")]
        [HttpGet]
        public async Task<ActionResult<List<ProdutoController>>> UltimasMovimentacoes(string dataInicio, string dataFinal)
        {
            DateTime dtInicio;
            DateTime dtFinal;

            if (!DateTime.TryParseExact(dataInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtInicio))
            {
                return BadRequest("Formato inválido para a data de início!! \nFormato esperado: 'yyyy-MM-dd'.");
            }

            if (!DateTime.TryParseExact(dataFinal, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFinal))
            {
                return BadRequest("Formato inválido para a data final!! \nFormato esperado: 'yyyy-MM-dd'.");
            }

            List<LogMovimentoModel> logMovimentos = await _logMovimentosRepositorio.UltimasMovimentacoes(dtInicio, dtFinal);
            return Ok(logMovimentos);
        }

        [Route("{dataInicio:datetime}/{dataFinal:datetime}/id={id}")]
        [HttpGet]
        public async Task<ActionResult<List<ProdutoController>>> UltimasMovimentacoesPorProduto(string dataInicio, string dataFinal, int id)
        {
            DateTime dtInicio;
            DateTime dtFinal;

            if (!DateTime.TryParseExact(dataInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtInicio))
            {
                return BadRequest("Formato inválido para a data de início!! \nFormato esperado: 'yyyy-MM-dd'.");
            }

            if (!DateTime.TryParseExact(dataFinal, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFinal))
            {
                return BadRequest("Formato inválido para a data final!! \nFormato esperado: 'yyyy-MM-dd'.");
            }

            List<LogMovimentoModel> logMovimentos = await _logMovimentosRepositorio.UltimasMovimentacoes(dtInicio, dtFinal, id);
            return Ok(logMovimentos);
        }
    }
}

