using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Achievements.Titles;

[PublicAPI]
public sealed class TitlesByIdsRequest : IHttpRequest<IReplicaSet<Title>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/titles")
    {
        AcceptEncoding = "gzip"
    };

    public TitlesByIdsRequest(IReadOnlyCollection<int> titleIds)
    {
        Check.Collection(titleIds, nameof(titleIds));
        TitleIds = titleIds;
    }

    public IReadOnlyCollection<int> TitleIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Title>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", TitleIds },
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

        var value = json.RootElement.GetSet(entry => TitleJson.GetTitle(entry, MissingMemberBehavior));
        return new ReplicaSet<Title>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
