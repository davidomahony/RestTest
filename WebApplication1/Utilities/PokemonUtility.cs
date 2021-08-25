using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Providers;
using WebApplication1.Translater;

namespace WebApplication1.Utilities
{
    public class PokemonUtility
    {
        private const string CaveHabitat = "cave";
        private IInformationFetcher<PokemonSpeciesModel> pokemonInformationProvider;
        private IEnumerable<IStringTranslater> availableTranslaters;

        public PokemonUtility(IInformationFetcher<PokemonSpeciesModel> informationProvider, IEnumerable<IStringTranslater> availableTranslaters)
        {
            this.pokemonInformationProvider = informationProvider;
            this.availableTranslaters = availableTranslaters;
        }

        public async Task<PokemonSpeciesModel> AcquireBasicInformation(string pokemonName, Guid requestId)
        {
            return await this.pokemonInformationProvider.FetchInformation(pokemonName);
        }

        public async Task<PokemonSpeciesModel> TranslateDescription(string pokemonName)
        {
            var informationToTranslate = await this.pokemonInformationProvider.FetchInformation(pokemonName);

            string description = await this.TranslateDescription(informationToTranslate);


            return informationToTranslate;
        }

        private async Task<string> TranslateDescription(PokemonSpeciesModel information)
        {
            try
            {
                var translater = this.IdentifyTranslater(information);

                return null;//await translater.TranslateTo(information.Description);
                
            }
            catch (Exception ex)
            {
                // Log me
                return null;//information.Description;
            }
        }

        private IStringTranslater IdentifyTranslater(PokemonSpeciesModel information)
        {
            if (information.IsLegendary || information.Habitat.Equals(CaveHabitat, System.StringComparison.OrdinalIgnoreCase))
            {
                return this.availableTranslaters.First(t => t.TranslaterIdentifier.Equals("YodaTranslater", StringComparison.OrdinalIgnoreCase));
            }

            return this.availableTranslaters.First(t => t.TranslaterIdentifier.Equals("ShakespeareTranslater", StringComparison.OrdinalIgnoreCase));
        }
    }
}
