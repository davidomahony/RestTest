using WebApplication1.Models;

namespace WebApplication1.Responses
{
    public class PokemonResponse : BaseResponse
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Habitat { get; set; }

        public bool IsLegendary { get; set; }
    }
}
