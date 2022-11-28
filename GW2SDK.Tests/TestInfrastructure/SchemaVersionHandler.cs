﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GuildWars2.Tests.TestInfrastructure;

internal class SchemaVersionHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        if (request.RequestUri?.Host == BaseAddress.DefaultUri.Host)
        {
            var version = request.RequestUri.Query.IndexOf(
                "v=" + SchemaVersion.Recommended,
                StringComparison.Ordinal
            );
            if (version == -1)
            {
                throw new InvalidOperationException(
                    "The request does not specify a schema version. Add a 'v' argument to the query string."
                );
            }

            if (version != request.RequestUri.Query.LastIndexOf("v=", StringComparison.Ordinal))
            {
                throw new InvalidOperationException("The 'v' argument can only be specified once.");
            }

            // Don't use UriBuilder because it contains breaking changes between .NET Framework and .NET (Core)
            // A simple string replace will have to suffice
            var requestUriWithLatestVersion = request.RequestUri.AbsoluteUri.Replace(
                SchemaVersion.Recommended,
                "3"
            );

            request.RequestUri = new Uri(requestUriWithLatestVersion, UriKind.Absolute);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
