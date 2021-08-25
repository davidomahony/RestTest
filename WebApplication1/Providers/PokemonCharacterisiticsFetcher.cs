using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Providers
{
    public class PokemonCharacterisiticsFetcher : HttpFetcher<PokemonCharacteristicsModel>
    {
        public PokemonCharacterisiticsFetcher(HttpClient client)
            : base(client, "PokemonCharacterisiticsFetcher")
        {
            client.BaseAddress = new Uri(@"https://pokeapi.co/api/v2/pokemon-species/");
        }

        public override PokemonCharacteristicsModel ConvertResult(object identifier)
        {
            throw new NotImplementedException();
        }

        public override Task<object> FetchInformation(string identifier)
        {
            throw new NotImplementedException();
        }
    }
}
