using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Banco.Models;
using Banco.Validacao;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContasController : ControllerBase
    {


        private readonly IBaseServico<Conta> _baseUserService;
        private readonly IBancos _bancos;

        public ContasController(IBaseServico<Conta> baseUserService, IBancos bancos)
        {
            _baseUserService = baseUserService;
            _bancos = bancos;
        }

        [HttpGet]
        public IActionResult Buscar()
        {
            var result = Execute(() => _baseUserService.Buscar<ListarContas>());
            return result;
        }


        [HttpGet("filtrar/{texto}")]
        public IActionResult Filtrar(string texto)
        {
            var result = _baseUserService.Buscar<ListarContas>();
            result = (from c in result
                      where (c.CodigoBanco.ToString().Trim().ToLower().Contains(texto.Trim().ToLower()))
                         || (c.Documento.ToString().Trim().ToLower().Contains(texto.Trim().ToLower())
                         || c.Nome.ToString().Trim().ToLower().Contains(texto.Trim().ToLower())
                         || c.NumeroAgencia.ToString().Trim().ToLower().Contains(texto.Trim().ToLower())
                         || c.NumeroConta.ToString().Trim().ToLower().Contains(texto.Trim().ToLower()))
                      select c).ToList();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Salvar(NovaConta conta)
        {
            return Execute(() => _baseUserService.Adicionar<NovaConta, Conta, NovaContaValidation>(conta));
        }

        [HttpPut]
        [Route("{contaId}")]
        public IActionResult Editar(AtualizarConta conta, int contaId)
        {
            return Execute(() => _baseUserService.Atualizar<AtualizarConta, Conta, EditarContaValidation>(conta));
        }


        [HttpGet("{contaId}")]
        public IActionResult BuscarPorId(int contaId)
        {
            if (contaId == 0)
                return NotFound();

            var resultado = Execute(() => _baseUserService.BuscarPorId<AtualizarConta>(contaId));
            return resultado;
        }


        [HttpDelete]
        [Route("{contaId}")]
        public IActionResult Excluir(int contaId)
        {
            Execute(() =>
            {
                _baseUserService.Remover(contaId);
                return true;
            });

            return Ok(new { mensagem = "Conta excluída com sucesso" });
        }


        [HttpGet]
        [Route("lista-bancos")]
        public async Task<IActionResult> ListarBancos()
        {
            var resultado = await _bancos.ListarBancos();
            return Ok(resultado);
        }

         
        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
