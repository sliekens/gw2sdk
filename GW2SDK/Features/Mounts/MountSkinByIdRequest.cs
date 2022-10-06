﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Mounts;

[PublicAPI]
public sealed class MountSkinByIdRequest : IHttpRequest<IReplica<MountSkin>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/mounts/skins")
    {
        AcceptEncoding = "gzip"
    };

    public MountSkinByIdRequest(int mountSkinId)
    {
        MountSkinId = mountSkinId;
    }

    public int MountSkinId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<MountSkin>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder { { "id", MountSkinId } },
                    AcceptLanguage = Language?.Alpha2Code
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetMountSkin(MissingMemberBehavior);
        return new Replica<MountSkin>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
