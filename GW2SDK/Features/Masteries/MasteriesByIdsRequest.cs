using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Masteries;

[PublicAPI]
public sealed class MasteriesByIdsRequest : IHttpRequest<IReplicaSet<Mastery>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/masteries")
    {
        AcceptEncoding = "gzip"
    };

    public MasteriesByIdsRequest(IReadOnlyCollection<int> masteryIds)
    {
        MasteryIds = masteryIds;
    }

    public IReadOnlyCollection<int> MasteryIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Mastery>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", MasteryIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => MasteryJson.GetMastery(entry, MissingMemberBehavior));
        return new ReplicaSet<Mastery>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
