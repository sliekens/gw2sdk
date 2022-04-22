using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Dungeons.Json;
using GW2SDK.Dungeons.Models;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Dungeons.Http;

[PublicAPI]
public sealed class DungeonsRequest : IHttpRequest<IReplicaSet<Dungeon>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "/v2/dungeons") { AcceptEncoding = "gzip" };


    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Dungeon>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("ids", "all");
        var request = Template with
        {
            Arguments = search,
            AcceptLanguage = Language?.Alpha2Code
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

        var value =
            json.RootElement.GetSet(entry => DungeonReader.Read(entry, MissingMemberBehavior));
        return new ReplicaSet<Dungeon>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
