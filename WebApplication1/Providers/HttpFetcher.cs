using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Interfaces;

namespace WebApplication1.Providers
{
    public abstract class HttpFetcher<T> : IInformationFetcher
    {
        protected HttpClient client;
        private readonly string fetcherIdentifier;

        public HttpFetcher(HttpClient client, string id)
        {
            this.client = client;
            this.fetcherIdentifier = id;
        }

        public abstract Task<object> FetchInformation(string identifier);

        public abstract T ConvertResult(object identifier);

        public string FetcherIdentifier()
        {
            return this.fetcherIdentifier;
        }
    }
}
