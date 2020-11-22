using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WebJetMovies.Application.Enums;

namespace WebJetMovies.Domain.Models
{
    //These are init only properties. Introduced in .Net 5. Ensures once they are populated they are not changed anywhere in the code.
    public record Movie
    {
        public string Title { get; init; }

        public string Year { get; init; }

        public string Rated { get; init; }

        public string Released { get; init; }

        public string Runtime { get; init; }

        public string Genre { get; init; }

        public string Director { get; init; }

        public string Writer { get; init; }

        public string Actors { get; init; }

        public string Plot { get; init; }

        public string Language { get; init; }

        public string Country { get; init; }

        public string Poster { get; init; }

        public string Metascore { get; init; }

        public string Rating { get; init; }

        public string Votes { get; init; }

        public string ID { get; init; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ItemType Type { get; init; }

        public double Price { get; init; }
    }
}