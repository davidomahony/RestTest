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
    public class ShakespeareTranslater : HttpFetcher<TranslatedModel>
    {
        public ShakespeareTranslater(HttpClient client)
            : base(client)
        {
        }

        //public async Task<string> TranslateTo(string input)
        //{
        //    try
        //    {
        //        var body = JsonConvert.SerializeObject(new { text = input });
        //        var content = new StringContent(body, Encoding.UTF8, "application/json");
        //        var result = await this.client.PostAsync(this.client.BaseAddress.ToString(), content);

        //        if (result.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            var responseContent = await result.Content.ReadAsStringAsync();
        //            var translatedObject = JsonConvert.DeserializeObject<TranslatedModel>(responseContent);

        //            // null check
        //            return null;
        //        }

        //        throw new Exception();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public override Task<HttpResponseMessage> RequestToExecute(string identifier, HttpContent requestContents = null)
        {
            var body = JsonConvert.SerializeObject(new { text = identifier });
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            return  this.client.PostAsync(this.client.BaseAddress.ToString(), content);
        }
    }

}
