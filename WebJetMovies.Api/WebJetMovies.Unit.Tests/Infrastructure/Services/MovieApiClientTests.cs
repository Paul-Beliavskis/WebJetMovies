using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Serilog;
using WebJetMovies.Application.Intefaces;
using WebJetMovies.Infrastructure.Configuration;
using WebJetMovies.Infrastructure.Services;
using Xunit;

namespace WebJetMovies.Unit.Tests.Infrastructure.Services
{
    public class MovieApiClientTests
    {
        private MovieApiClient _systemUnderTest;

        [Fact]
        public async Task GetFilmWorldMovieListAsync_DoesDataParseSuccess_DataFailedParsing()
        {

            var httpClientWrapper = A.Fake<IHttpClientWrapper>();
            A.CallTo(() => httpClientWrapper.GetStringAsync(A<string>.Ignored, A<CancellationToken>.Ignored))
            .Returns(Task.FromResult("not parable string"));

            var loggerFake = A.Fake<ILogger>();

            _systemUnderTest = new MovieApiClient(httpClientWrapper,
            Options.Create(new MovieApiUris()), Options.Create(new ApiRequestDetails()), loggerFake);

            var result = await _systemUnderTest.GetFilmWorldMovieListAsync(new CancellationToken());

            result.Should().NotBeNull();
            result.Movies.Should().BeNull();
        }

        [Fact]
        public async Task GetFilmWorldMovieListAsync_DoesReturnSuccess_HttpErrorReturned()
        {

            var httpClientWrapper = A.Fake<IHttpClientWrapper>();
            A.CallTo(() => httpClientWrapper.GetStringAsync(A<string>.Ignored, A<CancellationToken>.Ignored))
            .ThrowsAsync(new HttpRequestException());

            var loggerFake = A.Fake<ILogger>();

            _systemUnderTest = new MovieApiClient(httpClientWrapper,
            Options.Create(new MovieApiUris()), Options.Create(new ApiRequestDetails()), loggerFake);

            var result = await _systemUnderTest.GetFilmWorldMovieListAsync(new CancellationToken());

            result.Should().NotBeNull();
            result.Movies.Should().BeNull();
        }

        [Fact]
        public async Task GetFilmWorldMovieDetailAsync_DoesMovieReturn_MovieReturnSuccess()
        {

            var httpClientWrapper = A.Fake<IHttpClientWrapper>();
            A.CallTo(() => httpClientWrapper.GetStringAsync(A<string>.Ignored, A<CancellationToken>.Ignored))
            .Returns(Task.FromResult("{\r\n    \"Title\": \"Star Wars: Episode IV - A New Hope\",\r\n    \"Year\": \"1977\",\r\n    \"Rated\": \"PG\",\r\n    \"Released\": \"25 May 1977\",\r\n    \"Runtime\": \"121 min\",\r\n    \"Genre\": \"Action, Adventure, Fantasy\",\r\n    \"Director\": \"George Lucas\",\r\n    \"Writer\": \"George Lucas\",\r\n    \"Actors\": \"Mark Hamill, Harrison Ford, Carrie Fisher, Peter Cushing\",\r\n    \"Plot\": \"Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a wookiee and two droids to save the galaxy from the Empire's world-destroying battle-station, while also attempting to rescue Princess Leia from the evil Darth Vader.\",\r\n    \"Language\": \"English\",\r\n    \"Country\": \"USA\",\r\n    \"Poster\": \"http://ia.media-imdb.com/images/M/MV5BOTIyMDY2NGQtOGJjNi00OTk4LWFhMDgtYmE3M2NiYzM0YTVmXkEyXkFqcGdeQXVyNTU1NTfwOTk@._V1_SX300.jpg\",\r\n    \"Metascore\": \"92\",\r\n    \"Rating\": \"8.7\",\r\n    \"Votes\": \"915,459\",\r\n    \"ID\": \"fw0076759\",\r\n    \"Type\": \"movie\",\r\n    \"Price\": \"29.5\"\r\n}"));

            var loggerFake = A.Fake<ILogger>();

            _systemUnderTest = new MovieApiClient(httpClientWrapper,
            Options.Create(new MovieApiUris { GetFilmworldMovieDetail = "" }), Options.Create(new ApiRequestDetails()), loggerFake);

            var result = await _systemUnderTest.GetFilmWorldMovieDetailAsync("testId", new CancellationToken());

            result.Should().NotBeNull();
        }
    }
}
