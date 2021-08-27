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

        public async Task<string> TranslateTo(string input)
        {
            try
            {
                var content = new StringContent(@"{ \n text=""" + input + @""" \n }");
                var result = await this.client.PostAsync(this.client.BaseAddress.ToString(), content);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
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
