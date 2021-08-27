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
        private readonly IDictionary<string, IFetcher<TranslatedModel>> availableTranslaters;

        public PokemonUtility(IFetcher<PokemonSpeciesModel> informationProvider, IEnumerable<IFetcher<TranslatedModel>> availableTranslaters)
        {
            this.pokemonInformationFetcher = informationProvider;
            this.availableTranslaters = availableTranslaters.ToDictionary(item => item.GetType().Name, item => item); ;
        }

        public async Task<PokemonSpeciesModel> AcquireBasicInformation(string pokemonName, Guid requestId)
        {
            try
            {
                var speciesInformation = await pokemonInformationFetcher.FetchInfo(pokemonName);

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
                string result = englishDescription.flavor_text;
                if (string.IsNullOrEmpty(result))
                {
                    throw new NullReferenceException($"Description has no valid value, no need to query translation {requestId}");
                }

                bool useYoda = basicInfo.is_legendary || (basicInfo.habitat?.name?.Equals("cave", StringComparison.OrdinalIgnoreCase) == true);
                var translater = useYoda ?
                    this.availableTranslaters[typeof(YodaTranslater).Name] :
                    this.availableTranslaters[typeof(ShakespeareTranslater).Name];
                TranslatedModel translatedResult = await translater.FetchInfo(englishDescription.flavor_text);

                if (translatedResult != null && !string.IsNullOrEmpty(translatedResult?.contents?.translated))
                {
                    englishDescription.flavor_text = translatedResult.contents.translation;
                    return basicInfo;
                }

                throw new Exception($"Failed to retrieve valid translation {requestId}");
            }
            catch
            {
                return basicInfo;
            }
        }
    }
}
