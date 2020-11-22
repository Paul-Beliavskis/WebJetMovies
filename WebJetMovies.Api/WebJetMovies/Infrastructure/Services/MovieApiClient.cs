using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using WebJetMovies.Application.Intefaces;
using WebJetMovies.Domain.Models;
using WebJetMovies.Infrastructure.Configuration;
using WebJetMovies.Infrastructure.Dto;

namespace WebJetMovies.Infrastructure.Services
{
    public class MovieApiClient : IMovieApiClient
    {
        private readonly HttpClient _httpClient;

        private readonly MovieApiUris _movieApiUris;

        private readonly ApiRequestDetails _apiRequestDetails;

        private readonly ILogger _logger;

        public MovieApiClient(HttpClient httpClient,
        IOptions<MovieApiUris> movieApiUrisOptions,
        IOptions<ApiRequestDetails> apiRequestDetailsOptions,
        ILogger logger)
        {
            _httpClient = httpClient;

            _movieApiUris = movieApiUrisOptions.Value;

            _apiRequestDetails = apiRequestDetailsOptions.Value;

            _logger = logger;
        }

        public async Task<MovieListDto> GetFilmWorldMovieListAsync(CancellationToken cancelationToken)
        {
            try
            {
                var data = await _httpClient.GetStringAsync(_movieApiUris.GetFilmworldMovieList, CreateApiCancelationToken(cancelationToken));

                return JsonConvert.DeserializeObject<MovieListDto>(data);
            }
            catch (Exception e)
            {
                _logger.Error("Failed to retrieve list of FilmWorld movies", e);
            }

            return new MovieListDto();
        }

        public async Task<Movie> GetFilmWorldMovieDetailAsync(string movieId, CancellationToken cancelationToken)
        {
            try
            {
                var movieDetailUri = string.Format(_movieApiUris.GetFilmworldMovieDetail, movieId);

                var data = await _httpClient.GetStringAsync(movieDetailUri, CreateApiCancelationToken(cancelationToken));

                return JsonConvert.DeserializeObject<Movie>(data);
            }
            catch (Exception e)
            {
                _logger.Error($"Failed to retrieve details of FilmWorld movie {movieId}", e);
            }

            return null;
        }

        public async Task<MovieListDto> GetCinemaWorldMovieListAsync(CancellationToken cancelationToken)
        {

            try
            {
                var data = await _httpClient.GetStringAsync(_movieApiUris.GetCinemaworldMovieList, CreateApiCancelationToken(cancelationToken));

                return JsonConvert.DeserializeObject<MovieListDto>(data);
            }
            catch (Exception e)
            {
                _logger.Error("Failed to retrieve list of CinemaWorld movies", e);
            }

            return new MovieListDto();
        }

        public async Task<Movie> GetCinemaWorldMovieDetailAsync(string movieId, CancellationToken cancelationToken)
        {

            try
            {
                var movieDetailUri = string.Format(_movieApiUris.GetCinemaworldMovieDetail, movieId);

                var data = await _httpClient.GetStringAsync(movieDetailUri, CreateApiCancelationToken(cancelationToken));

                return JsonConvert.DeserializeObject<Movie>(data);
            }
            catch (Exception e)
            {
                _logger.Error($"Failed to retrieve details of CinemaWorld movie {movieId}", e);
            }

            return null;
        }

        private CancellationToken CreateApiCancelationToken(CancellationToken cancelationToken)
        {
            //Creates a timeout token that cancels the request after a certain amount of time
            using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(_apiRequestDetails.ApiRequestTimeOutInSeconds));

            //Link timeout token with the token passed in the request
            return CancellationTokenSource.CreateLinkedTokenSource(cancelationToken, cancellationTokenSource.Token).Token;
        }
    }
}
