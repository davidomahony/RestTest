using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Controllers;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Providers;
using WebApplication1.Responses;
using WebApplication1.Translater;
using WebApplication1.Utilities;

namespace UnitTests
{
    [TestClass]
    public class PokemonResponseGeneratorTests
    {
        private PokemonResponseGenerator responseGenerator;
        private Guid id = Guid.NewGuid();

        [TestInitialize]
        public void Setup()
        {
            var mockInformationProvider = new Mock<PokemonSpeciesFetcher>(null);
            mockInformationProvider.Setup(s => s.FetchInfo("ditto")).ReturnsAsync(TestUtility.TestPokeInfo);
            var mockYodaTranslater = new Mock<YodaTranslaterFetcher>(null);
            mockYodaTranslater.Setup(s => s.FetchInfo("Big long description")).ReturnsAsync(TestUtility.TestTranslatedInfo);
            var mockShakespheareTranslater = new Mock<ShakespeareTranslaterFetcher>(null);
            mockYodaTranslater.Setup(s => s.FetchInfo("Big long description")).ReturnsAsync(TestUtility.TestTranslatedInfo);

            var mockServiceUtility = new Mock<PokemonService>(mockInformationProvider.Object, new List<IFetcher<TranslatedModel>>()
            {
                mockYodaTranslater.Object
            });

            var stringInput = It.IsAny<string>();
            mockServiceUtility.Setup(s => s.AcquireBasicInformation("ditto", id)).ReturnsAsync(TestUtility.TestPokeInfo);
            mockServiceUtility.Setup(s => s.AcquireBasicInformation("exception", id)).Throws(new Exception());
            mockServiceUtility.Setup(s => s.AcquireTranslatedInformation("ditto", id)).ReturnsAsync(TestUtility.TestPokeInfo);
            mockServiceUtility.Setup(s => s.AcquireTranslatedInformation("exception", id)).Throws(new Exception());

            var mockLogger = new Mock<ILogger<PokemonController>>();
            this.responseGenerator = new PokemonResponseGenerator(mockServiceUtility.Object, mockLogger.Object);
        }

        [TestMethod]
        public void GenerateBasicInformationResponseValidInputTest()
        {
            var basicInfo = this.responseGenerator.GenerateBasicInformationResponse("ditto", id).Result;
            Assert.IsNotNull(basicInfo);
            Assert.AreEqual(basicInfo.RequestId, id);

            var pokeResponse = basicInfo as PokemonResponse;
            Assert.IsNotNull(pokeResponse);

            Assert.AreEqual(pokeResponse.Habitat, TestUtility.TestPokeInfo.habitat.name);
            Assert.AreEqual(pokeResponse.IsLegendary, TestUtility.TestPokeInfo.is_legendary);
            Assert.AreEqual(pokeResponse.Name, TestUtility.TestPokeInfo.name);
            Assert.AreEqual(pokeResponse.Description, TestUtility.TestPokeInfo.flavor_text_entries.ToList().First().flavor_text);
        }

        [TestMethod]
        public void GenerateBasicInformationResponseValidExceptionTest()
        {
            var basicInfo = this.responseGenerator.GenerateBasicInformationResponse("exception", id).Result;
            Assert.IsNotNull(basicInfo);
            Assert.AreEqual(basicInfo.RequestId, id);

            var errorResponse = basicInfo as ErrorResponse;
            Assert.IsNotNull(errorResponse);
            Assert.AreEqual(errorResponse.ErrorMessage, "We have no information on this Pokemon exception right now. Contact us with the request ID above to know more!");
        }

        [TestMethod]
        public void GenerateTranslationResponseValidInputTest()
        {
            var basicInfo = this.responseGenerator.GenerateTranslationResponse("ditto", id).Result;
            Assert.IsNotNull(basicInfo);
            Assert.AreEqual(basicInfo.RequestId, id);

            var pokeResponse = basicInfo as PokemonResponse;
            Assert.IsNotNull(pokeResponse);

            Assert.AreEqual(pokeResponse.Habitat, TestUtility.TestPokeInfo.habitat.name);
            Assert.AreEqual(pokeResponse.IsLegendary, TestUtility.TestPokeInfo.is_legendary);
            Assert.AreEqual(pokeResponse.Name, TestUtility.TestPokeInfo.name);
            Assert.AreEqual(pokeResponse.Description, TestUtility.TestPokeInfo.flavor_text_entries.ToList().First().flavor_text);
        }

        [TestMethod]
        public void GenerateTranslationResponseValidExceptionTest()
        {
            var basicInfo = this.responseGenerator.GenerateTranslationResponse("exception", id).Result;
            Assert.IsNotNull(basicInfo);
            Assert.AreEqual(basicInfo.RequestId, id);

            var errorResponse = basicInfo as ErrorResponse;
            Assert.IsNotNull(errorResponse);
            Assert.AreEqual(errorResponse.ErrorMessage, "We have no information on this Pokemon exception right now. Contact us with the request ID above to know more!");
        }
    }
}
