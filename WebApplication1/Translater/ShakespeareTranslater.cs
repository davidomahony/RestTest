using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
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

        public async Task<string> TranslateTo(string input)
        {
            try
            {
                var body = JsonConvert.SerializeObject(new { text = input });
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var result = await this.client.PostAsync(this.client.BaseAddress.ToString(), content);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseContent = await result.Content.ReadAsStringAsync();
                    var translatedObject = JsonConvert.DeserializeObject<TranslatedModel>(responseContent);

                    // null check
                    return null;
                }

                throw new Exception();
            }
            catch
            {
                throw;
            }
        }
    }

}
