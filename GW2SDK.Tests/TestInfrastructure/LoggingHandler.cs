using System.IO.Compression;
using System.Text;
using Xunit.Abstractions;

namespace GuildWars2.Tests.TestInfrastructure;

internal class LoggingHandler(ITestOutputHelper output) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        output.WriteLine($"{request.Method} {request.RequestUri!.PathAndQuery} HTTP/{request.Version}");
        foreach (var header in request.Headers)
        {
            output.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
        }

        if (request.Content is not null)
        {
            foreach (var header in request.Content.Headers)
            {
                output.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
        }

        output.WriteLine("");

        if (request.Content is not null)
        {
            output.WriteLine(await request.Content.ReadAsStringAsync());
            output.WriteLine("");
        }

        var response = await base.SendAsync(request, cancellationToken);

        output.WriteLine($"HTTP/{response.Version} {(int)response.StatusCode} {response.ReasonPhrase}");
        foreach (var header in response.Headers)
        {
            output.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
        }

        if (response.Content is not null)
        {
            foreach (var header in response.Content.Headers)
            {
                output.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
        }

        output.WriteLine("");

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
                    using var decompressed = new GZipStream(content, CompressionMode.Decompress, leaveOpen: true);
                    using var reader = new StreamReader(decompressed);
                    var text = await reader.ReadToEndAsync();
                    if (text.Length > 1024)
                    {
                        text = text.Substring(0, 1024) + "...";
                    }

                    output.WriteLine(text);
                }
                finally
                {
                    // ALSO IMPORTANT: rewind the stream for subsequent reads
                    content.Position = 0;
                }
            }
            else
            {
                output.WriteLine(await response.Content.ReadAsStringAsync());
            }

            output.WriteLine("");
        }

        return response;
    }
}
