using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Providers;
using WebApplication1.Translater;
using WebApplication1.Utilities;

namespace UnitTests
{
    [TestClass]
    public class PokemonServiceTests
    {
        private PokemonService pokemonService;

        [TestInitialize]
        public void Setup()
        {

            var mockInformationProvider = new Mock<PokemonSpeciesFetcher>(null);
            mockInformationProvider.Setup(s => s.FetchInfo("ditto")).ReturnsAsync(TestUtility.TestPokeInfo);
            mockInformationProvider.Setup(s => s.FetchInfo("snorlax")).ReturnsAsync(TestUtility.TestPokeInfoSnorlax);
            mockInformationProvider.Setup(s => s.FetchInfo("exception")).Throws(new PokemonNotFoundException("blah"));
            var mockYodaTranslater = new Mock<YodaTranslaterFetcher>(null);
            mockYodaTranslater.Setup(s => s.FetchInfo("Big long description")).ReturnsAsync(TestUtility.TestTranslatedInfo);
            var mockShakespheareTranslater = new Mock<ShakespeareTranslaterFetcher>(null);
            mockShakespheareTranslater.Setup(s => s.FetchInfo("snorlax description")).ReturnsAsync(TestUtility.TestTranslatedYodaInfo);

            this.pokemonService = new PokemonService(mockInformationProvider.Object, new List<IFetcher<TranslatedModel>>()
            {
                mockYodaTranslater.Object,
                mockShakespheareTranslater.Object
            });
        }

        [TestMethod]
        public void PokemonServiceBasicInformaitonTest()
        {
            var result = this.pokemonService.AcquireBasicInformation("ditto", Guid.Empty).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.habitat.name, TestUtility.TestPokeInfo.habitat.name);
            Assert.AreEqual(result.is_legendary, TestUtility.TestPokeInfo.is_legendary);
            Assert.AreEqual(result.name, TestUtility.TestPokeInfo.name);
            Assert.AreEqual(result.flavor_text_entries.First().flavor_text, TestUtility.TestPokeInfo.flavor_text_entries.ToList().First().flavor_text);
        }

        [TestMethod]
        public void PokemonServiceBasicInformaitonTestNotFound()
        {
            try
            {
                var result = this.pokemonService.AcquireBasicInformation("exception", Guid.Empty).Result;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex.InnerException, typeof(PokemonNotFoundException));
            }
        }

        [TestMethod]
        public void AcquireTranslatedInformationYodaTest()
        {
            var result = this.pokemonService.AcquireTranslatedInformation("ditto", Guid.Empty).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.habitat.name, TestUtility.TestPokeInfo.habitat.name);
            Assert.AreEqual(result.is_legendary, TestUtility.TestPokeInfo.is_legendary);
            Assert.AreEqual(result.name, TestUtility.TestPokeInfo.name);
            Assert.AreEqual(result.flavor_text_entries.First().flavor_text, TestUtility.TestTranslatedInfo.contents.translated);
        }

        [TestMethod]
        public void AcquireTranslatedInformationShapkesphere()
        {
            var result = this.pokemonService.AcquireTranslatedInformation("snorlax", Guid.Empty).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.habitat.name, TestUtility.TestPokeInfoSnorlax.habitat.name);
            Assert.AreEqual(result.is_legendary, TestUtility.TestPokeInfoSnorlax.is_legendary);
            Assert.AreEqual(result.name, TestUtility.TestPokeInfoSnorlax.name);
            Assert.AreEqual(result.flavor_text_entries.First().flavor_text, TestUtility.TestTranslatedYodaInfo.contents.translated);
        }

        [TestMethod]
        public void PokemonServiceAcquireTranslatedInformationTestNotFound()
        {
            try
            {
                var result = this.pokemonService.AcquireTranslatedInformation("exception", Guid.Empty).Result;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex.InnerException, typeof(PokemonNotFoundException));
            }
        }
    }
}
