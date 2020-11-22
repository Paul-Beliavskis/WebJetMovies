using System.Collections.Generic;
using WebJetMovies.Domain.Models;

namespace WebJetMovies.Application.Responses
{
    public class GetCheapestMoviesResponse
    {
        public List<Movie> Movies { get; set; }
    }
}
