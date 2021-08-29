using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
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
    public class PokemonControllerTests
    {

        private PokemonController controller;

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
            var guidInput = It.IsNotIn<Guid>(new List<Guid>() { Guid.Empty });
            var stringInput = It.IsAny<string>();
            mockServiceUtility.Setup(s => s.AcquireBasicInformation("ditto", guidInput)).ReturnsAsync(TestUtility.TestPokeInfo);
            mockServiceUtility.Setup(s => s.AcquireTranslatedInformation(stringInput, guidInput)).ReturnsAsync(TestUtility.TestPokeInfo);

            var mockLogger = new Mock<ILogger<PokemonController>>();
            var testResponseGenerator = new TestResponseGenerator(mockServiceUtility.Object, mockLogger.Object);
            this.controller = new PokemonController(mockLogger.Object, testResponseGenerator);
        }

        [TestMethod]
        public void BasicInformationInvalidInputsTest()
        {
            var result = this.controller.BasicInformation("").Result;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ErrorResponse));

            result = this.controller.BasicInformation(null).Result;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ErrorResponse));

            result = this.controller.BasicInformation("123").Result;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ErrorResponse));

            result = this.controller.BasicInformation("poke/poke").Result;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ErrorResponse));
        }

        [TestMethod]
        public void BasicInformationValidTest()
        {
            var result = this.controller.BasicInformation("valid").Result;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PokemonResponse));
        }

        [TestMethod]
        public void BasicInformationInvalidTest()
        {
            var result = this.controller.BasicInformation("ditto").Result;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ErrorResponse));
        }

        [TestMethod]
        public void TranslatedInformationInvalidInputsTest()
        {
            var result = this.controller.TranslatedInformation("").Result;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ErrorResponse));

            result = this.controller.TranslatedInformation(null).Result;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ErrorResponse));

            result = this.controller.TranslatedInformation("123").Result;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ErrorResponse));

            result = this.controller.TranslatedInformation("poke/poke").Result;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ErrorResponse));
        }

        [TestMethod]
        public void TranslatedInformationValidTest()
        {
            var result = this.controller.TranslatedInformation("valid").Result;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PokemonResponse));
        }

        [TestMethod]
        public void TranslatedInformationInvalidTest()
        {
            var result = this.controller.TranslatedInformation("ditto").Result;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ErrorResponse));
        }
    }
}
