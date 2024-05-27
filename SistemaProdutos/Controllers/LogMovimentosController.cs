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

        private static bool ValidaData(string dataInicio, string dataFinal, out DateTime dtInicio, out DateTime dtFinal)
        {
            if (!DateTime.TryParseExact(dataInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtInicio))
                throw new Exception("Formato inválido para a data de início!! \nFormato esperado: 'yyyy-MM-dd'.");

            if (!DateTime.TryParseExact(dataFinal, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFinal))
                throw new Exception("Formato inválido para a data final!! \nFormato esperado: 'yyyy-MM-dd'.");
            if (dtFinal < dtInicio)
                throw new Exception("Data final menor que data inicial. \nFavor revisar os parametros!!");

            return true;
        }

        [HttpGet]
        public async Task<ActionResult<List<LogMovimentoModel>>> UltimasMovimentacoes()
        {
            List<LogMovimentoModel> logMovimentos = await _logMovimentosRepositorio.UltimasMovimentacoes();
            return Ok(logMovimentos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<LogMovimentoModel>>> UltimasMovimentacoesPorProdutoId(int id)
        {
            List<LogMovimentoModel> logMovimentos = await _logMovimentosRepositorio.UltimasMovimentacoes(id);
            return Ok(logMovimentos);
        }

        [HttpGet("type={type}")]
        public async Task<ActionResult<List<LogMovimentoModel>>> UltimasMovimentacoesPorType(string type)
        {
            List<LogMovimentoModel> logMovimentos = await _logMovimentosRepositorio.UltimasMovimentacoes(type);
            return Ok(logMovimentos);
        }

        [HttpGet("id={id}/type={type}")]
        public async Task<ActionResult<List<LogMovimentoModel>>> UltimasMovimentacoesPorProdutoIdType(int id, string type)
        {
            List<LogMovimentoModel> logMovimentos = await _logMovimentosRepositorio.UltimasMovimentacoes(id, type);
            return Ok(logMovimentos);
        }

        [Route("{dataInicio:datetime}/{dataFinal:datetime}")]
        [HttpGet]
        public async Task<ActionResult<List<LogMovimentoModel>>> UltimasMovimentacoesPorData(string dataInicio, string dataFinal)
        {
            DateTime dtInicio;
            DateTime dtFinal;

            ValidaData(dataInicio, dataFinal, out dtInicio, out dtFinal);

            List<LogMovimentoModel> logMovimentos = await _logMovimentosRepositorio.UltimasMovimentacoes(dtInicio, dtFinal);
            return Ok(logMovimentos);
        }

        [Route("{dataInicio:datetime}/{dataFinal:datetime}/id={id}")]
        [HttpGet]
        public async Task<ActionResult<List<LogMovimentoModel>>> UltimasMovimentacoesPorProdutoIdData(string dataInicio, string dataFinal, int id)
        {
            DateTime dtInicio;
            DateTime dtFinal;

            ValidaData(dataInicio, dataFinal, out dtInicio, out dtFinal);

            List<LogMovimentoModel> logMovimentos = await _logMovimentosRepositorio.UltimasMovimentacoes(dtInicio, dtFinal, id);
            return Ok(logMovimentos);
        }

        [Route("{dataInicio:datetime}/{dataFinal:datetime}/id={id}/type={type}")]
        [HttpGet]
        public async Task<ActionResult<List<LogMovimentoModel>>> UltimasMovimentacoesPorProdutoDataTipo(string dataInicio, string dataFinal, int id, string type)
        {
            DateTime dtInicio;
            DateTime dtFinal;

            ValidaData(dataInicio, dataFinal, out dtInicio, out dtFinal);

            List<LogMovimentoModel> logMovimentos = await _logMovimentosRepositorio.UltimasMovimentacoes(dtInicio, dtFinal, id, type);
            return Ok(logMovimentos);
        }

        [Route("{dataInicio:datetime}/{dataFinal:datetime}/type={id}")]
        [HttpGet]
        public async Task<ActionResult<List<LogMovimentoModel>>> UltimasMovimentacoesPorDataTipo(string dataInicio, string dataFinal, string type)
        {
            DateTime dtInicio;
            DateTime dtFinal;

            ValidaData(dataInicio, dataFinal, out dtInicio, out dtFinal);

            List<LogMovimentoModel> logMovimentos = await _logMovimentosRepositorio.UltimasMovimentacoes(dtInicio, dtFinal, type);
            return Ok(logMovimentos);
        }
    }
}

