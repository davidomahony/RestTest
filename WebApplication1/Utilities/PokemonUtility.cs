using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Translater;

namespace WebApplication1.Utilities
{
    public class PokemonUtility
    {
        private readonly IFetcher<PokemonSpeciesModel> pokemonInformationFetcher;
        private readonly IDictionary<string, IStringTranslater> availableTranslaters;

        public PokemonUtility(IFetcher<PokemonSpeciesModel> informationProvider, IEnumerable<IStringTranslater> availableTranslaters)
        {
            this.pokemonInformationFetcher = informationProvider;
            this.availableTranslaters = availableTranslaters.ToDictionary(item => item.TranslaterIdentifier, item => item); ;
        }

        public async Task<PokemonSpeciesModel> AcquireBasicInformation(string pokemonName, Guid requestId)
        {
            try
            {
                var speciesInformation = await pokemonInformationFetcher.FetchInformation(pokemonName);

                return speciesInformation;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<PokemonSpeciesModel> AcquireTranslatedInformation(string pokemonName, Guid requestId)
        {
            try
            {
                var basicInfo = await this.AcquireBasicInformation(pokemonName, requestId);

                var translatedDescription = await this.TranslateDescription(basicInfo, requestId);

                return translatedDescription;
            }
            catch
            {
                throw;
            }
        }

        public async Task<PokemonSpeciesModel> TranslateDescription(PokemonSpeciesModel basicInfo, Guid requestId)
        {
            try
            {
                var englishDescription = basicInfo.flavor_text_entries.First(val => val.language.name.Equals("en", StringComparison.OrdinalIgnoreCase));

                bool useYoda = basicInfo.is_legendary || basicInfo.habitat.name.Equals("cave", StringComparison.OrdinalIgnoreCase);

                string result = englishDescription.flavor_text;
                if (this.availableTranslaters.TryGetValue(typeof(YodaTranslater).ToString(), out IStringTranslater yodaTranslater))
                {
                    result = await yodaTranslater.TranslateTo(englishDescription.flavor_text);
                }
                else if (this.availableTranslaters.TryGetValue(typeof(ShakespeareTranslater).ToString(), out IStringTranslater sTranslater))
                {
                    result = await sTranslater.TranslateTo(englishDescription.flavor_text);
                }

                englishDescription.flavor_text = result;
                return basicInfo;
            }
            catch
            {
                // log this
                return basicInfo;
            }
        }
    }
}
