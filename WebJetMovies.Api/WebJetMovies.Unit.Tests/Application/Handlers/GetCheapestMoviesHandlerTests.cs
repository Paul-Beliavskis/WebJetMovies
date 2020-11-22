using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using WebJetMovies.Application.Handlers;
using WebJetMovies.Application.Intefaces;
using WebJetMovies.Application.Requests;
using WebJetMovies.Domain.Models;
using WebJetMovies.Infrastructure.Dto;
using Xunit;

namespace WebJetMovies.Unit.Tests.Application.Handlers
{
    public class GetCheapestMoviesHandlerTests
    {
        private GetCheapestMoviesHandler _systemUnderTest;

        public GetCheapestMoviesHandlerTests()
        {
        }

        [Fact]
        public async Task Handle_AreMoviesReturned_MoviesReturnedSuccess()
        {
            //Arrange
            var cinemaWorldMovieListDto = new MovieListDto
            {
                Movies = new List<MovieDto>
                {
                    new MovieDto
                    {
                        Id = "fw17835",
                        Title = "The Great Gatsby"
                    }
                }
            };

            var cinemaWorldMovie = new Movie
            {
                ID = "fw17835",
                Title = "The Great Gatsby",
                Price = 32
            };

            var filmWorldMovieListDto = new MovieListDto
            {
                Movies = new List<MovieDto>
                {
                    new MovieDto
                    {
                        Id = "fw124235",
                        Title = "Avengers"
                    }
                }
            };

            var filmWorldMovie = new Movie
            {
                ID = "fw124235",
                Title = "Avengers",
                Price = 32
            };


            var request = new GetCheapestMoviesQuery();
            var movieApiClientFake = A.Fake<IMovieApiClient>();

            //Setup the calls to CinemaWorld endpoints
            _ = A.CallTo(() => movieApiClientFake.GetCinemaWorldMovieListAsync(A<CancellationToken>.Ignored))
                    .Returns(Task.FromResult(cinemaWorldMovieListDto));

            _ = A.CallTo(() => movieApiClientFake.GetCinemaWorldMovieDetailAsync(A<string>.Ignored, A<CancellationToken>.Ignored))
            .Returns(Task.FromResult(cinemaWorldMovie));

            //Setup the calls to MovieWorld enpoints
            _ = A.CallTo(() => movieApiClientFake.GetFilmWorldMovieListAsync(A<CancellationToken>.Ignored))
                    .Returns(Task.FromResult(filmWorldMovieListDto));

            _ = A.CallTo(() => movieApiClientFake.GetFilmWorldMovieDetailAsync(A<string>.Ignored, A<CancellationToken>.Ignored))
                        .Returns(Task.FromResult(filmWorldMovie));


            _systemUnderTest = new GetCheapestMoviesHandler(movieApiClientFake);

            //Act
            var response = await _systemUnderTest.Handle(request, new CancellationToken());

            //Assert
            response.Movies.Should().HaveCount(2);
        }

        [Fact]
        public async Task Handle_IsCheaperMovieReturned_OnlyCheaperMovieIsReturned()
        {
            var cheaperMovieId = "fw17835";

            //Arrange
            var cinemaWorldMovieListDto = new MovieListDto
            {
                Movies = new List<MovieDto>
                {
                    new MovieDto
                    {
                        Id = cheaperMovieId,
                        Title = "Avengers"
                    }
                }
            };

            var cinemaWorldMovie = new Movie
            {
                ID = cheaperMovieId,
                Title = "Avengers",
                Price = 12
            };

            var filmWorldMovieListDto = new MovieListDto
            {
                Movies = new List<MovieDto>
                {
                    new MovieDto
                    {
                        Id = "fw124235",
                        Title = "Avengers"
                    }
                }
            };

            var filmWorldMovie = new Movie
            {
                ID = "fw124235",
                Title = "Avengers",
                Price = 32
            };


            var request = new GetCheapestMoviesQuery();
            var movieApiClientFake = A.Fake<IMovieApiClient>();

            //Setup the calls to CinemaWorld endpoints
            _ = A.CallTo(() => movieApiClientFake.GetCinemaWorldMovieListAsync(A<CancellationToken>.Ignored))
                    .Returns(Task.FromResult(cinemaWorldMovieListDto));

            _ = A.CallTo(() => movieApiClientFake.GetCinemaWorldMovieDetailAsync(A<string>.Ignored, A<CancellationToken>.Ignored))
            .Returns(Task.FromResult(cinemaWorldMovie));

            //Setup the calls to MovieWorld enpoints
            _ = A.CallTo(() => movieApiClientFake.GetFilmWorldMovieListAsync(A<CancellationToken>.Ignored))
                    .Returns(Task.FromResult(filmWorldMovieListDto));

            _ = A.CallTo(() => movieApiClientFake.GetFilmWorldMovieDetailAsync(A<string>.Ignored, A<CancellationToken>.Ignored))
                        .Returns(Task.FromResult(filmWorldMovie));


            _systemUnderTest = new GetCheapestMoviesHandler(movieApiClientFake);

            //Act
            var response = await _systemUnderTest.Handle(request, new CancellationToken());

            //Assert
            response.Movies.Should().HaveCount(1);
            response.Movies.Should().OnlyContain(x => x.ID == cheaperMovieId);
        }



        [Fact]
        public async Task Handle_IsExpensiveMovieFiltered_ExpensiveMovieIsNotReturned()
        {
            var cheaperMovieId = "fw17835";
            var expensiveMovieId = "fw868723";

            //Arrange
            var cinemaWorldMovieListDto = new MovieListDto
            {
                Movies = new List<MovieDto>
                {
                    new MovieDto
                    {
                        Id = cheaperMovieId,
                        Title = "Avengers"
                    }
                }
            };

            var cinemaWorldMovie = new Movie
            {
                ID = cheaperMovieId,
                Title = "Avengers",
                Price = 12
            };

            var filmWorldMovieListDto = new MovieListDto
            {
                Movies = new List<MovieDto>
                {
                    new MovieDto
                    {
                        Id = expensiveMovieId,
                        Title = "Avengers"
                    }
                }
            };

            var filmWorldMovie = new Movie
            {
                ID = expensiveMovieId,
                Title = "Avengers",
                Price = 32
            };


            var request = new GetCheapestMoviesQuery();
            var movieApiClientFake = A.Fake<IMovieApiClient>();

            //Setup the calls to CinemaWorld endpoints
            _ = A.CallTo(() => movieApiClientFake.GetCinemaWorldMovieListAsync(A<CancellationToken>.Ignored))
                    .Returns(Task.FromResult(cinemaWorldMovieListDto));

            _ = A.CallTo(() => movieApiClientFake.GetCinemaWorldMovieDetailAsync(A<string>.Ignored, A<CancellationToken>.Ignored))
            .Returns(Task.FromResult(cinemaWorldMovie));

            //Setup the calls to MovieWorld enpoints
            _ = A.CallTo(() => movieApiClientFake.GetFilmWorldMovieListAsync(A<CancellationToken>.Ignored))
                    .Returns(Task.FromResult(filmWorldMovieListDto));

            _ = A.CallTo(() => movieApiClientFake.GetFilmWorldMovieDetailAsync(A<string>.Ignored, A<CancellationToken>.Ignored))
                        .Returns(Task.FromResult(filmWorldMovie));


            _systemUnderTest = new GetCheapestMoviesHandler(movieApiClientFake);

            //Act
            var response = await _systemUnderTest.Handle(request, new CancellationToken());

            //Assert
            response.Movies.Should().HaveCount(1);
            response.Movies.Should().NotContain(x => x.ID == expensiveMovieId);
        }
    }
}
