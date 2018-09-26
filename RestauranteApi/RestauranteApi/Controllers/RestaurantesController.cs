using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestauranteApi.Business;
using RestauranteApi.Models;

namespace RestauranteApi.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class RestaurantesController : ControllerBase
    {
        private readonly RestauranteService _service;

        public RestaurantesController(RestauranteService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Restaurante> GetPorNome()
        {
            return _service.ListarTodos();
        }

        [HttpGet("[action]/{restauranteId}")]
        public IActionResult Get(int restauranteId)
        {
            var restaurante = _service.Obter(restauranteId);
            if (restaurante != null)
                return new ObjectResult(restaurante);
            return NotFound();
        }

        [HttpGet("[action]/{nome}")]
        public IActionResult GetPorNome(string nome)
        {
            var restaurante = _service.ObterPorNome(nome);
            if (restaurante != null)
                return new ObjectResult(restaurante);
            return NotFound();
        }

        [HttpPost]
        public Resultado Post([FromBody]Restaurante restaurante)
        {
            return _service.Incluir(restaurante);
        }

        [HttpPut]
        public Resultado Put([FromBody]Restaurante restaurante)
        {
            return _service.Atualizar(restaurante);
        }

        [HttpDelete("{restauranteId}")]
        public Resultado Delete(int restauranteId)
        {
            return _service.Excluir(restauranteId);
        }
    }
}
