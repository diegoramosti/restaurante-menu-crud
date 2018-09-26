using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestauranteApi.Models
{
    public class Prato
    {
        public int PratoId { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int RestauranteId { get; set; }
        [JsonIgnore]
        public Restaurante Restaurante { get; set; }
    }
}
