using System.Collections.Generic;
using System.Linq;
using RestauranteApi.Data;
using RestauranteApi.Models;

namespace RestauranteApi.Business
{
    public class PratoService
    {

        private readonly ApplicationDbContext _context;

        public PratoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Prato Obter(int pratoId)
        {
            return pratoId > 0 ? _context.Pratos.FirstOrDefault(r => r.PratoId == pratoId) : null;
        }

        public List<Prato> ObterPorNome(string nome)
        {
            var valorLike = (nome ?? "").ToUpper();
            return _context.Pratos.Where(r => r.Nome.ToUpper().Contains(valorLike)).ToList();
        }

        public IEnumerable<Prato> ListarTodos()
        {
            return _context.Pratos.OrderBy(p => p.Nome).ToList();
        }

        public Resultado Incluir(Prato dadosPrato)
        {
            var resultado = DadosValidos(dadosPrato);
            resultado.Acao = "Inclusão de Prato";

            if (resultado.Inconsistencias.Count == 0)
            {
                _context.Pratos.Add(dadosPrato);
                _context.SaveChanges();
            }
            return resultado;
        }

        public Resultado Excluir(int pratoId)
        {
            var resultado = new Resultado { Acao = "Exclusão de Prato" };

            var prato = Obter(pratoId);
            if (prato == null)
            {
                resultado.Inconsistencias.Add(
                    "Prato não encontrado");
            }
            else
            {
                _context.Pratos.Remove(prato);
                _context.SaveChanges();
            }
            return resultado;
        }

        public Resultado Atualizar(Prato dadosPrato)
        {
            var resultado = DadosValidos(dadosPrato);
            resultado.Acao = "Atualização de Prato";

            if (resultado.Inconsistencias.Count == 0)
            {
                var prato = _context.Pratos.FirstOrDefault(r => r.PratoId == dadosPrato.PratoId);

                if (prato == null)
                {
                    resultado.Inconsistencias.Add(
                        "Prato não encontrado");
                }
                else
                {
                    prato.Nome = dadosPrato.Nome;
                    prato.RestauranteId = dadosPrato.RestauranteId;
                    prato.Preco = dadosPrato.Preco;
                    _context.SaveChanges();
                }
            }

            return resultado;
        }

        private static Resultado DadosValidos(Prato prato)
        {
            var resultado = new Resultado();
            if (prato == null)
            {
                resultado.Inconsistencias.Add(
                    "Preencha os dados do Prato");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(prato.Nome))
                {
                    resultado.Inconsistencias.Add(
                        "Preencha o nome");
                }
                if (prato.Preco <= 0)
                {
                    resultado.Inconsistencias.Add(
                        "Preço inválido");
                }
            }
            return resultado;
        }
    }
}
