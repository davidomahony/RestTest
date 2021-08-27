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

        public override Task<HttpResponseMessage> RequestToExecute(string identifier, HttpContent contents = null)
        {
            return this.client.GetAsync(identifier);
        }
    }
}
