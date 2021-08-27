using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
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
                    return translatedObject.contents.translated;
                }

                // check result

                return null;
            }
            catch
            {
                throw;
            }
        }
    }
}
