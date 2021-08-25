using WebApplication1.Models;

namespace WebApplication1.Responses
{
    public class PokemonResponse
    {
        public PokemonResponse(PokemonCharacteristicsModel characteristic, PokemonSpeciesModel species)
            : base ()
        {
            this.Name = species.Name;
            //this.Description = characteristic.Description
            this.Habitat = species.Habitat;
            this.IsLegendary = species.IsLegendary;
        }

        public string Name { get; }

        public string Description { get; }

        public string Habitat { get; }

        public bool IsLegendary { get; }
    }
}
