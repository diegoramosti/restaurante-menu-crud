using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestauranteApi.Data;
using RestauranteApi.Models;

namespace RestauranteApi.Business
{
    public class RestauranteService
    {
        private readonly ApplicationDbContext _context;

        public RestauranteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Restaurante Obter(int restauranteId)
        {
            return restauranteId > 0 ? _context.Restaurantes.FirstOrDefault(r => r.RestauranteId == restauranteId) : null;
        }

        public List<Restaurante> ObterPorNome(string nome)
        {
            var valorLike = (nome ?? "").ToUpper();
            return _context.Restaurantes.Where(r=> r.Nome.ToUpper().Contains(valorLike)).ToList();
        }

        public IEnumerable<Restaurante> ListarTodos()
        {
            return _context.Restaurantes.OrderBy(p => p.Nome).ToList();
        }

        public Resultado Incluir(Restaurante dadosRestaurante)
        {
            var resultado = DadosValidos(dadosRestaurante);
            resultado.Acao = "Inclusão de Restaurante";

            if (resultado.Inconsistencias.Count == 0)
            {
                _context.Restaurantes.Add(dadosRestaurante);
                _context.SaveChanges();
            }
            return resultado;
        }

        public Resultado Excluir(int restauranteId)
        {
            var resultado = new Resultado { Acao = "Exclusão de Restaurante" };

            var restaurante = Obter(restauranteId);
            if (restaurante == null)
            {
                resultado.Inconsistencias.Add(
                    "Restaurante não encontrado");
            }
            else
            {
                _context.Restaurantes.Remove(restaurante);
                _context.SaveChanges();
            }
            return resultado;
        }

        public Resultado Atualizar(Restaurante dadosRestaurante)
        {
            var resultado = DadosValidos(dadosRestaurante);
            resultado.Acao = "Atualização de Restaurante";

            if (resultado.Inconsistencias.Count == 0)
            {
                var restaurante = _context.Restaurantes.FirstOrDefault(r => r.RestauranteId == dadosRestaurante.RestauranteId);

                if (restaurante == null)
                {
                    resultado.Inconsistencias.Add(
                        "Restaurante não encontrado");
                }
                else
                {
                    restaurante.Nome = dadosRestaurante.Nome;
                    _context.SaveChanges();
                }
            }

            return resultado;
        }


        private static Resultado DadosValidos(Restaurante restaurante)
        {
            var resultado = new Resultado();
            if (restaurante == null)
            {
                resultado.Inconsistencias.Add(
                    "Preencha os Dados do Restaurante");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(restaurante.Nome))
                {
                    resultado.Inconsistencias.Add(
                        "Preencha o nome");
                }
            }
            return resultado;
        }
    }
}
