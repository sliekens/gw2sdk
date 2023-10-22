using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Files.Http;

[PublicAPI]
public sealed class FilesByIdsRequest : IHttpRequest<Replica<HashSet<Asset>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/files")
    {
        AcceptEncoding = "gzip"
    };

    public FilesByIdsRequest(IReadOnlyCollection<string> fileIds)
    {
        Check.Collection(fileIds);
        FileIds = fileIds;
    }

    public IReadOnlyCollection<string> FileIds { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Asset>>> SendAsync(
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
        return new Replica<HashSet<Asset>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetAsset(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
