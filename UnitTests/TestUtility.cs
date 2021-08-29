using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Controllers;
using WebApplication1.Models;
using WebApplication1.Responses;
using WebApplication1.Utilities;

namespace UnitTests
{
    public static class TestUtility
    {
        public static PokemonSpeciesModel TestPokeInfo = new PokemonSpeciesModel()
        {
            name = "ditto",
            habitat = new PokemonSpeciesModel.Habitat()
            {
                name = "test set up",
                url = "dummy"
            },
            is_legendary = true,
            flavor_text_entries = new List<PokemonSpeciesModel.FlavorTextEntries>()
            {
                new PokemonSpeciesModel.FlavorTextEntries()
                {
                    language = new PokemonSpeciesModel.FlavorTextEntries.DescriptionLanguage()
                    {
                        name = "en",
                        url = "dummy"
                    },
                    flavor_text = "Big long description"
                }
            }
        };

        public static PokemonSpeciesModel TestPokeInfoSnorlax = new PokemonSpeciesModel()
        {
            name = "snorlax",
            habitat = new PokemonSpeciesModel.Habitat()
            {
                name = "test set up snorlax",
                url = "dummy"
            },
            is_legendary = false,
            flavor_text_entries = new List<PokemonSpeciesModel.FlavorTextEntries>()
            {
                new PokemonSpeciesModel.FlavorTextEntries()
                {
                    language = new PokemonSpeciesModel.FlavorTextEntries.DescriptionLanguage()
                    {
                        name = "en",
                        url = "dummy"
                    },
                    flavor_text = "snorlax description"
                }
            }
        };

        public static TranslatedModel TestTranslatedInfo = new TranslatedModel()
        {
            contents =
            new TranslatedModel.Contents()
            {
                translation = "test",
                translated = "actually translated"
            }
        };

        public static TranslatedModel TestTranslatedYodaInfo = new TranslatedModel()
        {
            contents =
            new TranslatedModel.Contents()
            {
                translated = "wise be you",
                translation = "dummmy"
            }
        };
    }

    public class TestResponseGenerator : PokemonResponseGenerator
    {
        public TestResponseGenerator(PokemonService service, ILogger<PokemonController> logger)
            : base(service, logger)
        {
        }

        public async override Task<BaseResponse> GenerateBasicInformationResponse(string pokemonName, Guid requestId)
        {
            return TestResponse(pokemonName);
        }

        public async override Task<BaseResponse> GenerateTranslationResponse(string pokemonName, Guid requestId)
        {
            return TestResponse(pokemonName);
        }

        private static BaseResponse TestResponse(string pokemonName)
        {
            return pokemonName.Equals("valid") ? new PokemonResponse()
            {
                RequestId = Guid.Empty,
                RequestTime = DateTime.UtcNow,
                Description = TestUtility.TestPokeInfo.flavor_text_entries.ToList().First().flavor_text,
                Habitat = TestUtility.TestPokeInfo.habitat.name,
                IsLegendary = TestUtility.TestPokeInfo.is_legendary
            } :
            (BaseResponse)new ErrorResponse()
            {
                RequestId = Guid.Empty,
                RequestTime = DateTime.UtcNow,
                ErrorMessage = "error"
            };
        }
    }
}
