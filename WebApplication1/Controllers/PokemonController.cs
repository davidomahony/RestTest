using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private ILogger<PokemonController> logger;
        private PokemonResponseGenerator pokemonUtility;

        public PokemonController(ILogger<PokemonController> logger, PokemonUtility ut)
        {
            this.pokemonUtility = new PokemonResponseGenerator(ut, logger);
            this.logger = logger;
        }

        [HttpGet("{pokemonName}")]
        public async Task<BaseResponse> BasicInformation([Required] string pokemonName)
        {
            Guid requestId = Guid.NewGuid();

            this.logger.LogInformation($"PokemonName: {pokemonName}, RequsetId: {requestId}, Call: BasicInformation, Time:{DateTimeOffset.UtcNow}");

            return await this.pokemonUtility.GenerateBasicInformationResponse(pokemonName, requestId);
        }

        [HttpGet("translated/{pokemonName}")]
        public async Task<BaseResponse> TranslatedInformation([Required] string pokemonName)
        {
            Guid requestId = Guid.NewGuid();

            this.logger.LogInformation($"PokemonName: {pokemonName}, RequsetId: {requestId}, Call: TranslatedInformation, Time:{DateTimeOffset.UtcNow}");

            return await this.pokemonUtility.GenerateTranslationResponse(pokemonName, requestId);
        }
    }
}
