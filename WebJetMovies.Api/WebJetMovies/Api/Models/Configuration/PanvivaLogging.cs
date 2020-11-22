
namespace WebJetMovies.Api.Models.Configuration
{
    public class PanvivaLogging
    {
        public static string LoggingConfigName = "PanvivaLogging";

        public string ApplicationName { get; set; }

        public BlobStorage BlobStorage { get; set; }

        public string ContainerName { get; set; }

        public string SeqUrl { get; set; }
    }

    public class BlobStorage
    {
        public string ConnectionString { get; set; }
    }
}
