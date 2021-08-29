using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Responses;
using WebApplication1.Utilities;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private const string InvalidInputMsg = "The input provided has been deeemed invalid, ensure a valid pokemon name is used";
        private readonly ILogger<PokemonController> logger;
        private readonly PokemonResponseGenerator responseGenerator;

        public PokemonController(ILogger<PokemonController> logger, PokemonResponseGenerator responseGenerator)
        {
            this.responseGenerator = responseGenerator;
            this.logger = logger;
        }

        [HttpGet("{pokemonName}")]
        public async Task<BaseResponse> BasicInformation(string pokemonName)
        {
            Guid requestId = Guid.NewGuid();

            if (!this.IsInputValid(pokemonName))
            {
                return this.responseGenerator.GenerateErrorResponse(requestId, InvalidInputMsg);
            }

            this.logger.LogInformation($"PokemonName: {pokemonName}, RequsetId: {requestId}, Call: BasicInformation, Time:{DateTimeOffset.UtcNow}");

            return await this.responseGenerator.GenerateBasicInformationResponse(pokemonName, requestId); ;
        }

        [HttpGet("translated/{pokemonName}")]
        public async Task<BaseResponse> TranslatedInformation(string pokemonName)
        {
            Guid requestId = Guid.NewGuid();

            if (!this.IsInputValid(pokemonName))
            {
                return this.responseGenerator.GenerateErrorResponse(requestId, InvalidInputMsg);
            }

            this.logger.LogInformation($"PokemonName: {pokemonName}, RequsetId: {requestId}, Call: TranslatedInformation, Time:{DateTimeOffset.UtcNow}");

            return await this.responseGenerator.GenerateTranslationResponse(pokemonName, requestId);
        }

        private bool IsInputValid(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            // As pokemon names can only be letters
            if (input.ToCharArray().Any(c => !char.IsLetter(c)))
            {
                return false;
            }

            return true;
        }
    }
}
