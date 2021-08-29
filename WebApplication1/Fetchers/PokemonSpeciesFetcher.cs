using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Providers
{
    /// <summary>
    /// Fetches species information from external api
    /// </summary>
    public class PokemonSpeciesFetcher : HttpFetcher<PokemonSpeciesModel>
    {
        public PokemonSpeciesFetcher(HttpClient client)
            : base(client)
        {
            this.FetcherIdentifier = typeof(PokemonSpeciesFetcher).Name;
        }

        protected override Task<HttpResponseMessage> RequestToExecute(string identifier)
        {
            return this.client.GetAsync(identifier);
        }

        protected override void HandleNonSuccessResponse(HttpResponseMessage message, string pokemonId)
        {
            if (message.ReasonPhrase.Equals("Not Found", System.StringComparison.OrdinalIgnoreCase))
            {
                throw new PokemonNotFoundException(pokemonId);
            }
        }
    }
}
