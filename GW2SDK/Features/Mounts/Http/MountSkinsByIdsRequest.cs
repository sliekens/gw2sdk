using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Mounts.Http;

internal sealed class MountSkinsByIdsRequest : IHttpRequest<Replica<HashSet<MountSkin>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/mounts/skins")
    {
        AcceptEncoding = "gzip"
    };

    public MountSkinsByIdsRequest(IReadOnlyCollection<int> mountSkinIds)
    {
        Check.Collection(mountSkinIds);
        MountSkinIds = mountSkinIds;
    }

    public IReadOnlyCollection<int> MountSkinIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<MountSkin>>> SendAsync(
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
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<HashSet<MountSkin>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetMountSkin(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
