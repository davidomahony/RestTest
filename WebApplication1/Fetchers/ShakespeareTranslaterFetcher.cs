using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Providers;

namespace WebApplication1.Translater
{
    /// <summary>
    /// Fetch shaesphere translation from external api
    /// </summary>
    public class ShakespeareTranslaterFetcher : HttpFetcher<TranslatedModel>
    {
        public ShakespeareTranslaterFetcher(HttpClient client)
            : base(client)
        {
            this.FetcherIdentifier = typeof(ShakespeareTranslaterFetcher).Name;
        }

        protected override Task<HttpResponseMessage> RequestToExecute(string identifier)
        {
            var body = JsonConvert.SerializeObject(new { text = identifier });
            var content = new StringContent(body, Encoding.UTF8, apllicationJsonMediaType);
            var test =   this.client.PostAsync("shakespeare.json", content);

            return test;
        }
    }

}
