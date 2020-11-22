using System.Threading;
using System.Threading.Tasks;
using WebJetMovies.Domain.Models;
using WebJetMovies.Infrastructure.Dto;

namespace WebJetMovies.Application.Intefaces
{
    public interface IMovieApiClient
    {
        Task<MovieListDto> GetFilmWorldMovieListAsync(CancellationToken cancelationToken);

        Task<MovieListDto> GetCinemaWorldMovieListAsync(CancellationToken cancelationToken);

        Task<Movie> GetCinemaWorldMovieDetailAsync(string movieId, CancellationToken cancelationToken);

        Task<Movie> GetFilmWorldMovieDetailAsync(string movieId, CancellationToken cancelationToken);
    }
}
