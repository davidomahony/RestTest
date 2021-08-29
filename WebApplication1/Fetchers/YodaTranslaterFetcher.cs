using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Providers;

namespace WebApplication1.Utilities
{
    public class YodaTranslaterFetcher : HttpFetcher<TranslatedModel>
    {
        public YodaTranslaterFetcher(HttpClient client)
            : base(client)
        {
            this.FetcherIdentifier = typeof(YodaTranslaterFetcher).Name;
        }

        protected override Task<HttpResponseMessage> RequestToExecute(string identifier)
        {
            var body = JsonConvert.SerializeObject(new { text = identifier });
            var content = new StringContent(body, Encoding.UTF8, apllicationJsonMediaType);
            return this.client.PostAsync("yoda.json", content);
        }
    }
}
