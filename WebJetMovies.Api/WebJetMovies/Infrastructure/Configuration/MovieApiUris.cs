namespace WebJetMovies.Infrastructure.Configuration
{
    public record MovieApiUris
    {
        public string GetCinemaworldMovieList { get; init; }

        public string GetFilmworldMovieList { get; init; }

        public string GetCinemaworldMovieDetail { get; init; }

        public string GetFilmworldMovieDetail { get; set; }
    }
}
