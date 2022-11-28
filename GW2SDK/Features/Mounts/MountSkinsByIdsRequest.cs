using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Mounts;

[PublicAPI]
public sealed class MountSkinsByIdsRequest : IHttpRequest<IReplicaSet<MountSkin>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/mounts/skins")
    {
        AcceptEncoding = "gzip"
    };

    public MountSkinsByIdsRequest(IReadOnlyCollection<int> mountSkinIds)
    {
        Check.Collection(mountSkinIds, nameof(mountSkinIds));
        MountSkinIds = mountSkinIds;
    }

    public IReadOnlyCollection<int> MountSkinIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<MountSkin>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", MountSkinIds },
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

        var value = json.RootElement.GetSet(entry => MountSkinJson.GetMountSkin(entry, MissingMemberBehavior));
        return new ReplicaSet<MountSkin>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
