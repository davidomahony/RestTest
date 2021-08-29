using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Interfaces;

namespace WebApplication1.Providers
{
    public abstract class HttpFetcher<T> : IFetcher<T>
    {
        protected const string apllicationJsonMediaType = "application/json";
        protected HttpClient client;
        protected string identifier;

        public string FetcherIdentifier { get; protected set; }

        public HttpFetcher(HttpClient client) => this.client = client;

        public virtual async Task<T> FetchInfo(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
            {
                throw new ArgumentNullException("Null or empty value inputted for Http fetcher");
            }

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

                this.HandleNonSuccessResponse(result, identifier);

                return default;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected abstract Task<HttpResponseMessage> RequestToExecute(string identifier);

        protected virtual void HandleNonSuccessResponse(HttpResponseMessage message, string identifier)
        {
            throw new System.Exception($"Failed to fetch informaiton for identifier: {identifier}," +
                $" Resulting Status Code: {message.StatusCode}, reason: {message.ReasonPhrase}");
        }
    }
}
