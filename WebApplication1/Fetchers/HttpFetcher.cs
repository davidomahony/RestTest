using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Interfaces;

namespace WebApplication1.Providers
{
    public abstract class HttpFetcher<T> : IFetcher<T>
    {
        protected HttpClient client;

        public HttpFetcher(HttpClient client) => this.client = client;

        public async Task<T> FetchInfo(string identifier)
        {
            try
            {
                var result = await this.RequestToExecute(identifier);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseContent = await result.Content.ReadAsStringAsync();
                    var deserializedResponse = JsonConvert.DeserializeObject<T>(responseContent);

                    if (deserializedResponse != null)
                    {
                        return deserializedResponse;
                    }
                }

                return default(T);
            }
            catch
            {
                throw;
            }
        }

        public abstract Task<HttpResponseMessage> RequestToExecute(string identifier, HttpContent requestContents = null);
    }
}
