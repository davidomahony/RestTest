using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using WebApplication1.Utilities;

namespace UnitTests
{
    [TestClass]
    public class YodaFetcherTests
    {
        private YodaTranslaterFetcher fetcher;

        [TestInitialize]
        public void Setup()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(@"https://api.funtranslations.com/translate/yoda.json");
            this.fetcher = new YodaTranslaterFetcher(httpClient);
        }

        [TestMethod]
        public void YodaFetcherValidInputTest()
        {
            var basicInfo = this.fetcher.FetchInfo("This is a test my friend").Result;
            Assert.IsNotNull(basicInfo);
            Assert.AreEqual(basicInfo.contents.translated, "A test my friend,  this is");
        }

        [TestMethod]
        public void YodaFetcherEmptyInputTest()
        {
            try
            {
                var basicInfo = this.fetcher.FetchInfo(string.Empty).Result;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void YodaFetcherNullInputTest()
        {
            try
            {
                var basicInfo = this.fetcher.FetchInfo(null).Result;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentNullException));
            }
        }
    }
}
