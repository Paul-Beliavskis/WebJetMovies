using System.Threading;
using System.Threading.Tasks;

namespace WebJetMovies.Application.Intefaces
{
    public interface IHttpClientWrapper
    {
        Task<string> GetStringAsync(string? requestUri, CancellationToken cancellationToken);
    }
}
