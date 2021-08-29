using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Controllers;
using WebApplication1.Models;
using WebApplication1.Responses;

namespace WebApplication1.Utilities
{
    public class PokemonResponseGenerator
    {
        const string pokemonNotFoungErrMsg = "The pokemon specifieds information does not exist.";
        private readonly string loggingMessage = "Issue with request {0} at {1} with input: {2}";
        private readonly PokemonService pokemonUtility;
        private readonly ILogger<PokemonController> logger;

        public PokemonResponseGenerator(PokemonService pokemonUtility, ILogger<PokemonController> logger)
        {
            this.logger = logger;
            this.pokemonUtility = pokemonUtility;
        }

        public virtual async Task<BaseResponse> GenerateBasicInformationResponse(string pokemonName, Guid requestId)
        {
            try
            {
                var info = await pokemonUtility.AcquireBasicInformation(pokemonName, requestId);
                return this.GenerateBasicInfoResponse(info, requestId);
            }
            catch (PokemonNotFoundException ex)
            {
                logger.LogInformation(string.Format(loggingMessage, requestId, DateTime.UtcNow, pokemonName));
                logger.LogInformation($"Due to pokemon not found exception: {ex.Message}");
                return this.GenerateErrorResponse(requestId, pokemonNotFoungErrMsg);
            }
            catch (Exception ex)
            {
                logger.LogInformation(string.Format(loggingMessage, requestId, DateTime.UtcNow, pokemonName));
                logger.LogInformation($"Due to exception: {ex.Message}");
                return this.GenerateErrorResponse(pokemonName, requestId);
            }
        }

        public virtual async Task<BaseResponse> GenerateTranslationResponse(string pokemonName, Guid requestId)
        {
            try
            {
                var info = await pokemonUtility.AcquireTranslatedInformation(pokemonName, requestId);
                return this.GenerateBasicInfoResponse(info, requestId);
            }
            catch (PokemonNotFoundException ex)
            {
                logger.LogInformation(string.Format(loggingMessage, requestId, DateTime.UtcNow, pokemonName));
                logger.LogInformation($"Due to pokemon not found exception: {ex.Message}");
                return this.GenerateErrorResponse(requestId, pokemonNotFoungErrMsg);
            }
            catch (Exception ex)
            {
                logger.LogInformation(string.Format(loggingMessage, requestId, DateTime.UtcNow, pokemonName));
                logger.LogInformation($"Due to exception: {ex.Message}");
                return this.GenerateErrorResponse(pokemonName, requestId);
            }
        }

        public BaseResponse GenerateErrorResponse(Guid requestId, string errMsg)
        {
            return new ErrorResponse()
            {
                RequestId = requestId,
                RequestTime = DateTime.UtcNow,
                ErrorMessage = errMsg,
            };
        }

        private BaseResponse GenerateBasicInfoResponse(PokemonSpeciesModel info, Guid requestId)
        {
            return new PokemonResponse()
            {
                RequestId = requestId,
                RequestTime = DateTime.UtcNow,
                Name = info.name,
                Habitat = info.habitat?.name ?? string.Empty, 
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
