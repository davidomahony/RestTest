using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PokemonSpeciesModel
    {
        public string Name { get; set; }

        public long Id { get; set; }

        public string Habitat { get; set; }

        public bool IsLegendary { get; set; }
    }
}
