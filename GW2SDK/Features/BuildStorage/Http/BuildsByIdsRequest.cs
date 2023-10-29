using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.BuildStorage.Http;

internal sealed class BuildsByIdsRequest : IHttpRequest<Replica<HashSet<Build>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/account/buildstorage") { AcceptEncoding = "gzip" };

    public BuildsByIdsRequest(IReadOnlyCollection<int> buildStorageIds)
    {
        BuildStorageIds = buildStorageIds;
    }

    public IReadOnlyCollection<int> BuildStorageIds { get; }

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Build>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", BuildStorageIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    BearerToken = AccessToken
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<HashSet<Build>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetBuild(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
