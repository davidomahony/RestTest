using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Providers
{
    public class PokemonSpeciesFetcher : HttpFetcher<PokemonSpeciesModel>
    {
        public PokemonSpeciesFetcher(HttpClient client)
            : base(client)
        {
        }

        public async override Task<PokemonSpeciesModel> FetchInformation(string identifier)
        {
            try
            {
                var result = await this.client.GetAsync(identifier);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var pokemonSpecies = JsonConvert.DeserializeObject<PokemonSpeciesModel>(content);

                    // null check
                    return pokemonSpecies;
                }

                // check result

                return null;
            }
            catch
            {
                throw;
            }
        }
    }
}
