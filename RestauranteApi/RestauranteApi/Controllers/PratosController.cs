using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestauranteApi.Business;
using RestauranteApi.Models;

namespace RestauranteApi.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class PratosController : ControllerBase
    {
        private readonly PratoService _service;

        public PratosController(PratoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Prato> Get()
        {
            return _service.ListarTodos();
        }

        [HttpGet("[action]/{pratoId}")]
        public IActionResult Get(int pratoId)
        {
            var prato = _service.Obter(pratoId);
            if (prato != null)
                return new ObjectResult(prato);
            return NotFound();
        }

        [HttpGet("[action]/{nome}")]
        public IActionResult GetPorNome(string nome)
        {
            var prato = _service.ObterPorNome(nome);
            if (prato != null)
                return new ObjectResult(prato);
            return NotFound();
        }

        [HttpPost]
        public Resultado Post([FromBody]Prato prato)
        {
            return _service.Incluir(prato);
        }

        [HttpPut]
        public Resultado Put([FromBody]Prato prato)
        {
            return _service.Atualizar(prato);
        }

        [HttpDelete("{PratoId}")]
        public Resultado Delete(int pratoId)
        {
            return _service.Excluir(pratoId);
        }
    }
}
