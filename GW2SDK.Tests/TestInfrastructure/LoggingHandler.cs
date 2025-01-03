﻿using System.IO.Compression;

namespace GuildWars2.Tests.TestInfrastructure;

internal class LoggingHandler : DelegatingHandler
{
    public static AsyncLocal<ITestOutputHelper> Output { get; } = new();

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        Output.Value?.WriteLine(
            $"{request.Method} {request.RequestUri!.PathAndQuery} HTTP/{request.Version}"
        );
        foreach (var header in request.Headers)
        {
            Output.Value?.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
        }

        if (request.Content is not null)
        {
            foreach (var header in request.Content.Headers)
            {
                Output.Value?.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
        }

        Output.Value?.WriteLine("");

        if (request.Content is not null)
        {
            Output.Value?.WriteLine(await request.Content.ReadAsStringAsync());
            Output.Value?.WriteLine("");
        }

        var response = await base.SendAsync(request, cancellationToken);

        Output.Value?.WriteLine(
            $"HTTP/{response.Version} {(int)response.StatusCode} {response.ReasonPhrase}"
        );
        foreach (var header in response.Headers)
        {
            Output.Value?.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
        }

        if (response.Content is not null)
        {
            foreach (var header in response.Content.Headers)
            {
                Output.Value?.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
        }

        Output.Value?.WriteLine("");

        if (response.Content is not null)
        {
            if (response.Content.Headers.ContentEncoding.LastOrDefault() == "gzip")
            {
                // IMPORTANT: buffer the Content to make ReadAsStreamAsync return a rewindable MemoryStream
                await response.Content.LoadIntoBufferAsync();

                // ALSO IMPORTANT: do not dispose the MemoryStream because subsequent ReadAsStreamAsync calls return the same instance
                var content = await response.Content.ReadAsStreamAsync();

                try
                {
                    using var decompressed = new GZipStream(
                        content,
                        CompressionMode.Decompress,
                        true
                    );
                    using var reader = new StreamReader(decompressed);
                    var text = await reader.ReadToEndAsync();
                    if (text.Length > 1024)
                    {
                        text = text.Substring(0, 1024) + "...";
                    }

                    Output.Value?.WriteLine(text);
                }
                finally
                {
                    // ALSO IMPORTANT: rewind the stream for subsequent reads
                    content.Position = 0;
                }
            }
            else
            {
                Output.Value?.WriteLine(await response.Content.ReadAsStringAsync());
            }

            Output.Value?.WriteLine("");
        }

        return response;
    }
}
