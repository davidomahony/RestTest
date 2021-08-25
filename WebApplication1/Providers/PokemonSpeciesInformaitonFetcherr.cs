using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Providers
{
    public class PokemonSpeciesInformaitonFetcherr : HttpFetcher<PokemonSpeciesModel>
    {
        public PokemonSpeciesInformaitonFetcherr(HttpClient client)
            : base (client)
        {
            client.BaseAddress = new Uri(@"https://pokeapi.co/api/v2/pokemon-species/");
        }

        public async override Task<PokemonSpeciesModel> FetchInformation(string identifier)
        {
            try
            {
                var result = await this.client.GetAsync(identifier);
                return null;
            }
            catch
            {
                throw;
            }
        }
    }
}
