using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using WebApplication1;
using WebApplication1.Providers;

namespace UnitTests
{
    [TestClass]
    public class PokemonSPeciesFetcherTests
    {
        private PokemonSpeciesFetcher speciesFetcher;

        [TestInitialize]
        public void Setup()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(@"https://pokeapi.co/api/v2/pokemon-species/");
            this.speciesFetcher = new PokemonSpeciesFetcher(httpClient);         
        }

        [TestMethod]
        public void PokemonSpeciesFetcherValidPokemonTest()
        {
            var basicInfo = this.speciesFetcher.FetchInfo("ditto").Result;
            Assert.IsNotNull(basicInfo);
            Assert.AreEqual(basicInfo.name, "ditto");
            Assert.AreEqual(basicInfo.is_legendary, false);
            Assert.IsNotNull(basicInfo.flavor_text_entries);
            Assert.IsNotNull(basicInfo.habitat);
        }

        [TestMethod]
        public void PokemonSpeciesFetcherInValidPokemonTest()
        {
            try
            {
                var basicInfo = this.speciesFetcher.FetchInfo("zzzzzz").Result;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex.InnerException, typeof(PokemonNotFoundException));
            }
        }

        [TestMethod]
        public void PokemonSpeciesFetcherEmptyString()
        {
            try
            {
                var basicInfo = this.speciesFetcher.FetchInfo(string.Empty).Result;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void PokemonSpeciesFetcherNull()
        {
            try
            {
                var basicInfo = this.speciesFetcher.FetchInfo(string.Empty).Result;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentNullException));
            }
        }
    }
}
