using System.IO.Compression;

namespace GuildWars2.Tests.TestInfrastructure;

internal sealed class LoggingHandler : DelegatingHandler
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
        foreach (KeyValuePair<string, IEnumerable<string>> header in request.Headers)
        {
            Output.Value?.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
        }

        if (request.Content is not null)
        {
            foreach (KeyValuePair<string, IEnumerable<string>> header in request.Content.Headers)
            {
                Output.Value?.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
        }

        Output.Value?.WriteLine("");

        if (request.Content is not null)
        {
            Output.Value?.WriteLine(await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));
            Output.Value?.WriteLine("");
        }

        HttpResponseMessage? response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

        Output.Value?.WriteLine(
            $"HTTP/{response.Version} {(int)response.StatusCode} {response.ReasonPhrase}"
        );
        foreach (KeyValuePair<string, IEnumerable<string>> header in response.Headers)
        {
            Output.Value?.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
        }

        if (response.Content is not null)
        {
            foreach (KeyValuePair<string, IEnumerable<string>> header in response.Content.Headers)
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
                await response.Content.LoadIntoBufferAsync(cancellationToken).ConfigureAwait(false);

                // ALSO IMPORTANT: do not dispose the MemoryStream because subsequent ReadAsStreamAsync calls return the same instance
                Stream? content = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

                try
                {
                    using GZipStream decompressed = new(
                        content,
                        CompressionMode.Decompress,
                        true
                    );
                    using StreamReader reader = new(decompressed);
                    string? text = await reader.ReadToEndAsync(cancellationToken).ConfigureAwait(false);
                    if (text.Length > 1024)
                    {
#if NET
                        text = string.Concat(text.AsSpan(0, 1024), "...");
#else
                        text = text[..1024] + "...";
#endif
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
                Output.Value?.WriteLine(await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));
            }

            Output.Value?.WriteLine("");
        }

        return response;
    }
}
