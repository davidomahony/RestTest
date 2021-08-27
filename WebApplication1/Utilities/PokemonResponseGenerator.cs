using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Controllers;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Providers;
using WebApplication1.Responses;
using WebApplication1.Translater;

namespace WebApplication1.Utilities
{
    public class PokemonResponseGenerator
    {
        private readonly PokemonUtility pokemonUtility;
        private readonly ILogger logger;

        public PokemonResponseGenerator(PokemonUtility pokemonUtility, ILogger logger)
        {
            this.logger = logger;
            this.pokemonUtility = pokemonUtility;
        }

        public async Task<BaseResponse> GenerateBasicInformationResponse(string pokemonName, Guid requestId)
        {
            try
            {
                var info = await pokemonUtility.AcquireBasicInformation(pokemonName, requestId);
                return this.GenerateBasicInfoResponse(info, requestId);
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Issue with request {requestId} at {DateTime.UtcNow} with input: {pokemonName} due to {ex.Message}");
                return this.GenerateErrorResponse(pokemonName, requestId);
            }
        }

        public async Task<BaseResponse> GenerateTranslationResponse(string pokemonName, Guid requestId)
        {
            try
            {
                var info = await pokemonUtility.AcquireTranslatedInformation(pokemonName, requestId);
                return this.GenerateBasicInfoResponse(info, requestId);
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Issue with request {requestId} at {DateTime.UtcNow} with input: {pokemonName} due to {ex.Message}");
                return this.GenerateErrorResponse(pokemonName, requestId);
            }
        }

        private BaseResponse GenerateBasicInfoResponse(PokemonSpeciesModel info, Guid requestId)
        {
            return new PokemonResponse()
            {
                RequestId = requestId,
                RequestTime = DateTime.UtcNow,
                Name = info.name,
                Habitat = info.habitat.name, 
                Description = info.flavor_text_entries.First(val => val.language.name.Equals("en", StringComparison.OrdinalIgnoreCase))?.flavor_text ?? string.Empty,
                IsLegendary = info.is_legendary
            };
        }

        private BaseResponse GenerateErrorResponse(string pokemonName, Guid requestId)
        {
            return new ErrorResponse()
            {
                RequestId = requestId,
                RequestTime = DateTime.UtcNow,
                ErrorMessage = $"We have no information on this Pokemon {pokemonName} right now. Contact us with the request ID above to know more!"
            };
        }
    }
}
