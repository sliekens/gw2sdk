using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Files;

[PublicAPI]
public sealed class FilesByIdsRequest : IHttpRequest<Replica<HashSet<File>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(HttpMethod.Get, "v2/files")
    {
        AcceptEncoding = "gzip"
    };

    public FilesByIdsRequest(IReadOnlyCollection<string> fileIds)
    {
        Check.Collection(fileIds, nameof(fileIds));
        FileIds = fileIds;
    }

    public IReadOnlyCollection<string> FileIds { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<File>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", FileIds },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<HashSet<File>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetFile(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
