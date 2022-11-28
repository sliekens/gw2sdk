using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.BuildStorage;

[PublicAPI]
public sealed class BuildsByIdsRequest : IHttpRequest<IReplicaSet<Build>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/account/buildstorage") { AcceptEncoding = "gzip" };

    public BuildsByIdsRequest(IReadOnlyCollection<int> buildStorageIds)
    {
        BuildStorageIds = buildStorageIds;
    }

    public IReadOnlyCollection<int> BuildStorageIds { get; }

    public string? AccessToken { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Build>> SendAsync(
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
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => BuildJson.GetBuild(entry, MissingMemberBehavior));
        return new ReplicaSet<Build>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
