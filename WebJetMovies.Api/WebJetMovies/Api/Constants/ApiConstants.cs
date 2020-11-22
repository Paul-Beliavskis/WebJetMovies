namespace WebJetMovies.Api.Constants
{
    internal static class ApiConstants
    {
        internal static class Defaults
        {
            internal const string DefaultPage = "index.html";
        }

        internal static class RequestHeaders
        {
            internal const string AccessTokenHeaderName = "X-Access-Token";
        }

        internal static class ConfigurationNames
        {
            internal const string AccessToken = "Api:AccessToken";

            internal const string ApiServerAddress = "Api:ApiServerAddress";

            internal const string MovieApiUris = "Api:MovieApiUris";

            internal const string ApiRequestDetails = "Api:ApiRequestDetails";
        }
    }
}
