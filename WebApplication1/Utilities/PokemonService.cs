using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Translater;

namespace WebApplication1.Utilities
{
    public class PokemonService
    {
        private readonly IFetcher<PokemonSpeciesModel> pokemonInformationFetcher;
        private readonly IList<IFetcher<TranslatedModel>> availableTranslaters;

        public PokemonService(IFetcher<PokemonSpeciesModel> informationProvider, IEnumerable<IFetcher<TranslatedModel>> availableTranslaters)
        {
            this.pokemonInformationFetcher = informationProvider;
            this.availableTranslaters = availableTranslaters.ToList();
        }

        public virtual async Task<PokemonSpeciesModel> AcquireBasicInformation(string pokemonName, Guid requestId)
        {
            var speciesInformation = await pokemonInformationFetcher.FetchInfo(pokemonName);

            if (speciesInformation == null)
            {
                throw new Exception("Invalid species information returned");
            }

            return speciesInformation;
        }

        public virtual async Task<PokemonSpeciesModel> AcquireTranslatedInformation(string pokemonName, Guid requestId)
        {
            var basicInfo = await this.AcquireBasicInformation(pokemonName, requestId);

            var translatedDescription = await this.TranslateDescription(basicInfo, requestId);

            if (translatedDescription == null)
            {
                throw new Exception("Invalid description information returned");
            }

            return translatedDescription;
        }

        private async Task<PokemonSpeciesModel> TranslateDescription(PokemonSpeciesModel basicInfo, Guid requestId)
        {
            // If for some reason we fail to get the description the standard description can still be returned
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
                    this.availableTranslaters.First(tr => tr.FetcherIdentifier.Equals(typeof(YodaTranslaterFetcher).Name, StringComparison.OrdinalIgnoreCase)) :
                    this.availableTranslaters.First(tr => tr.FetcherIdentifier.Equals(typeof(ShakespeareTranslaterFetcher).Name, StringComparison.OrdinalIgnoreCase));

                if (translater == null)
                {
                    throw new NullReferenceException("Unable to find translater");
                }

                TranslatedModel translatedResult = await translater.FetchInfo(englishDescription.flavor_text);

                string newDescription = translatedResult?.contents?.translated;
                if (translatedResult != null && !string.IsNullOrEmpty(newDescription))
                {
                    englishDescription.flavor_text = newDescription;
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
