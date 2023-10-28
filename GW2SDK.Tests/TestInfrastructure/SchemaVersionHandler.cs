namespace GuildWars2.Tests.TestInfrastructure;

internal class SchemaVersionHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        // For testing, the latest schema version is used to detect API breaking changes
        // For production builds, GW2SDK is locked to a specific schema version
        // This handler does 2 things:
        // - Throw an error if the request does not use the recommended schema version
        // - Rewrite the request to use the latest schema version
        if (request.RequestUri?.Host == BaseAddress.DefaultUri.Host)
        {
            var recommended = SchemaVersion.Recommended;
            var version = request.RequestUri.Query.IndexOf(
                "v=" + recommended,
                StringComparison.Ordinal
            );
            if (version == -1)
            {
                throw new InvalidOperationException(
                    "The request does not use the recommended schema version. Add a 'v' argument using SchemaVersion.Recommended."
                );
            }

            if (version != request.RequestUri.Query.LastIndexOf("v=", StringComparison.Ordinal))
            {
                throw new InvalidOperationException("The 'v' argument can only be specified once.");
            }

            // Don't use UriBuilder because it contains breaking changes between .NET Framework and .NET (Core)
            // A simple string replace will have to suffice
            var requestUriWithLatestVersion = request.RequestUri.AbsoluteUri.Replace(
                recommended,
                "3"
            );

            request.RequestUri = new Uri(requestUriWithLatestVersion, UriKind.Absolute);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
