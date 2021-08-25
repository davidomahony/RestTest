using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Interfaces;

namespace WebApplication1.Providers
{
    public abstract class HttpFetcher<T> : IInformationFetcher<T>
    {
        protected HttpClient client;
        protected string baseUrl;

        public HttpFetcher(HttpClient client) => this.client = client;

        public abstract Task<T> FetchInformation(string identifier);
    }
}
