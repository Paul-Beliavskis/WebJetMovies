using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebJetMovies.Application.Intefaces;

namespace WebJetMovies.Infrastructure.Services
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<string> GetStringAsync(string? requestUri, CancellationToken cancellationToken)
        {
            return _httpClient.GetStringAsync(requestUri, cancellationToken);
        }
    }
}
