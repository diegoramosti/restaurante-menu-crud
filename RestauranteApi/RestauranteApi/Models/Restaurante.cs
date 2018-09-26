using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestauranteApi.Models
{
    public class Restaurante
    {
        public int RestauranteId { get; set; }
        public string Nome { get; set; }
        [JsonIgnore]
        public List<Prato> Pratos { get; set; }
    }
}
