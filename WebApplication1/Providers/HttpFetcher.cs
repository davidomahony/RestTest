using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Interfaces;

namespace WebApplication1.Providers
{
    public abstract class HttpFetcher<T> : IFetcher<T>
    {
        protected HttpClient client;

        public HttpFetcher(HttpClient client) => this.client = client;

        public abstract Task<T> FetchInformation(string identifier);
    }
}
