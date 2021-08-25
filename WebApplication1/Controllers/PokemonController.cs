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
        private ILogger<PokemonSpeciesModel> logger;
        private PokemonUtility pokemonUtility;

        public PokemonController(ILogger<PokemonSpeciesModel> logger, IInformationFetcher<PokemonSpeciesModel> informationProvider, IEnumerable<IStringTranslater> translaters)
        {
            this.pokemonUtility = new PokemonUtility(informationProvider, translaters);
            this.logger = logger;
        }

        [HttpGet("{pokemonName}")]
        public async Task<BaseResponse> BasicInformation([Required] string pokemonName)
        {
            Guid requestId = Guid.NewGuid();
            this.logger.LogInformation($"PokemonName: {pokemonName}, RequsetId: {requestId}, Call: BasicInformation, Time:{DateTimeOffset.UtcNow}");
            return null;
        }

        [HttpGet("translated/{pokemonName}")]
        public async Task<BaseResponse> TranslatedInformation([Required] string pokemonName)
        {
            Guid requestId = Guid.NewGuid();
            this.logger.LogInformation($"PokemonName: {pokemonName}, RequsetId: {requestId}, Call: TranslatedInformation, Time:{DateTimeOffset.UtcNow}");
            return null;
        }
    }
}
