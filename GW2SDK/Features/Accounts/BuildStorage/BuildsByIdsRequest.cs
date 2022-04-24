using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.BuildStorage;

[PublicAPI]
public sealed class BuildsByIdsRequest : IHttpRequest<IReplicaSet<Build>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "/v2/account/buildstorage") { AcceptEncoding = "gzip" };

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
        QueryBuilder search = new();
        search.Add("ids", BuildStorageIds);
        var request = Template with
        {
            Arguments = search,
            BearerToken = AccessToken
        };

        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
                )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => BuildReader.Read(entry, MissingMemberBehavior));
        return new ReplicaSet<Build>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
