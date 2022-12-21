using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Items;

[PublicAPI]
public sealed class ItemsByIdsRequest : IHttpRequest<IReplicaSet<Item>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/items")
    {
        AcceptEncoding = "gzip"
    };

    public ItemsByIdsRequest(IReadOnlyCollection<int> itemIds)
    {
        Check.Collection(itemIds, nameof(itemIds));
        ItemIds = itemIds;
    }

    public IReadOnlyCollection<int> ItemIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Item>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    AcceptLanguage = Language?.Alpha2Code,
                    Arguments = new QueryBuilder
                    {
                        { "ids", ItemIds },
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

        return new ReplicaSet<Item>
        {
            Values = json.RootElement.GetSet(entry => entry.GetItem(MissingMemberBehavior)),
            Context = response.Headers.GetCollectionContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
