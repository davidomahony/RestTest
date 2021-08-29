using System.Threading.Tasks;

namespace WebApplication1.Interfaces
{
    public interface IFetcher<T>
    {
        Task<T> FetchInfo(string identifier);

        string FetcherIdentifier { get; }
    }
}
