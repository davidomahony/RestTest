using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Interfaces
{
    public interface IInformationFetcher
    {
        Task<object> FetchInformation(string identifier);

        string FetcherIdentifier();
    }
}
