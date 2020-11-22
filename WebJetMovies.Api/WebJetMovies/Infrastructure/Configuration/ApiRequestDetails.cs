namespace WebJetMovies.Infrastructure.Configuration
{
    public record ApiRequestDetails
    {
        public int ApiRequestTimeOutInSeconds { get; init; }
    }
}
