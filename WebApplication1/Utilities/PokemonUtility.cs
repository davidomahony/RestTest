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
        private IDictionary<string, IInformationFetcher> pokemonInformationFetchers;
        private IDictionary<string, IStringTranslater> availableTranslaters;

        public PokemonUtility(IEnumerable<IInformationFetcher> informationProvider, IEnumerable<IStringTranslater> availableTranslaters)
        {
            this.pokemonInformationFetchers = informationProvider.ToDictionary(item => item.FetcherIdentifier(), item => item);
            this.availableTranslaters = availableTranslaters.ToDictionary(item => item.TranslaterIdentifier, item => item); ;
        }

        public async Task<BasicPokemonModel> AcquireBasicInformation(string pokemonName, Guid requestId)
        {
            try
            {
                bool locatedSpeciesFetcher = pokemonInformationFetchers.TryGetValue("PokemonSpeciesInformaitonFetcher", out IInformationFetcher speciesFetcher);
                bool locatedCharacteristicsFetcher = pokemonInformationFetchers.TryGetValue("PokemonCharacterisiticsFetcher", out IInformationFetcher characterisiticsFetcher);

                if (!locatedCharacteristicsFetcher || !locatedSpeciesFetcher)
                {
                    // return some error
                }

                var speciesInformation = await speciesFetcher.FetchInformation(pokemonName);
                var characteristicInfo = await characterisiticsFetcher.FetchInformation(pokemonName);

                if (characteristicInfo is PokemonCharacteristicsModel charInfo && speciesInformation is PokemonSpeciesModel specInfo)
                {
                    var pokemonBuilder = new BasicPokemonInformationBuilder();
                    pokemonBuilder.SetCharacteristicInfo(charInfo);
                    pokemonBuilder.SetSpeciesInfo(specInfo);
                    return pokemonBuilder.GetResult();
                }

                // throw exception
            }
            catch
            {

            }
            return null;
        }

        public async Task<BasicPokemonModel> TranslateDescription(string pokemonName, Guid requestId)
        {
            //var informationToTranslate = await this.pokemonInformationFetchers.FetchInformation(pokemonName);

            // string description = await this.TranslateDescription(informationToTranslate);


            return null; //informationToTranslate;
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
            return null;
            //if (information.IsLegendary || information.Habitat.Equals(CaveHabitat, System.StringComparison.OrdinalIgnoreCase))
            //{
            //    return this.availableTranslaters.First(t => t.TranslaterIdentifier.Equals("YodaTranslater", StringComparison.OrdinalIgnoreCase));
            //}

            //return this.availableTranslaters.First(t => t.TranslaterIdentifier.Equals("ShakespeareTranslater", StringComparison.OrdinalIgnoreCase));
        }
    }
}
