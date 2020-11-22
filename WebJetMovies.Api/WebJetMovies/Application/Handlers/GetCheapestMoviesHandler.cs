using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebJetMovies.Application.Enums;
using WebJetMovies.Application.Intefaces;
using WebJetMovies.Application.Requests;
using WebJetMovies.Application.Responses;
using WebJetMovies.Domain.Models;

namespace WebJetMovies.Application.Handlers
{
    public class GetCheapestMoviesHandler : IRequestHandler<GetCheapestMoviesQuery, GetCheapestMoviesResponse>
    {
        private readonly IMovieApiClient _movieApiClient;

        public GetCheapestMoviesHandler(IMovieApiClient movieApiClient)
        {
            _movieApiClient = movieApiClient;
        }

        public async Task<GetCheapestMoviesResponse> Handle(GetCheapestMoviesQuery request, CancellationToken cancellationToken)
        {
            var movieListBackgroundTasks = new List<Task<List<Task<Movie>>>>
            {
                _movieApiClient.GetCinemaWorldMovieListAsync(cancellationToken)
                                .ContinueWith(x => GetMovieDetails(x.Result?.Movies
                                               ?.Select(x => x.Id)
                                               ?.ToList(), MovieApi.Cinemaworld,cancellationToken)),

                _movieApiClient.GetFilmWorldMovieListAsync(cancellationToken)
                                .ContinueWith(x => GetMovieDetails(x.Result?.Movies
                                                ?.Select(x => x.Id)
                                                ?.ToList(), MovieApi.Filmworld,cancellationToken))

            };

            var completedFetchListTasks = await Task.WhenAll(movieListBackgroundTasks);

            var fetchMovieDetailsTasks = completedFetchListTasks.Where(x => x != null).SelectMany(x => x).Select(x => x);

            await Task.WhenAll(fetchMovieDetailsTasks);

            var filteredMovies = FilterCheapestMovies(fetchMovieDetailsTasks.Where(x => x.Result != null).Select(x => x.Result).ToList());

            return new GetCheapestMoviesResponse
            {
                Movies = filteredMovies
            };
        }

        private List<Movie> FilterCheapestMovies(List<Movie> movies)
        {
            var orderedMovieList = movies.OrderBy(x => x.Price);

            var filteredMovies = new Dictionary<string, Movie>();

            foreach (var movie in orderedMovieList)
            {

                if (filteredMovies.ContainsKey(movie.Title))
                {
                    continue;
                }

                filteredMovies.Add(movie.Title, orderedMovieList.Where(x => x.Title == movie.Title).FirstOrDefault());
            }

            return filteredMovies.Select(x => x.Value).ToList();
        }

        private List<Task<Movie>> GetMovieDetails(List<string> movieIds, MovieApi movieApi, CancellationToken cancellationToken)
        {
            if (movieIds == null || !movieIds.Any())
            {
                return null;
            }

            var movieDetailTasks = new List<Task<Movie>>();

            foreach (var movieId in movieIds)
            {
                if (movieApi == MovieApi.Cinemaworld)
                {
                    movieDetailTasks.Add(_movieApiClient.GetCinemaWorldMovieDetailAsync(movieId, cancellationToken));
                }
                else if (movieApi == MovieApi.Filmworld)
                {
                    movieDetailTasks.Add(_movieApiClient.GetFilmWorldMovieDetailAsync(movieId, cancellationToken));
                }
                else
                {
                    break;
                }
            }
            return movieDetailTasks;
        }
    }
}
