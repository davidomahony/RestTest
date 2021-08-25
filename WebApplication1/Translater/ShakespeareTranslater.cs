using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Translater
{
    public class ShakespeareTranslater : IStringTranslater
    {
        private HttpClient client;

        public string TranslaterIdentifier => typeof(ShakespeareTranslater).ToString();

        public ShakespeareTranslater(HttpClient client) => this.client = client;

        public Task<string> TranslateTo(string input)
        {
            throw new NotImplementedException();
        }
    }

}
