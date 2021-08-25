using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Utilities
{
    public class YodaTranslater : IStringTranslater
    {
        private HttpClient client;
        public string TranslaterIdentifier => typeof(YodaTranslater).ToString();

        public YodaTranslater(HttpClient client) => this.client = client;

        public Task<string> TranslateTo(string input)
        {
            throw new System.NotImplementedException();
        }
    }
}
