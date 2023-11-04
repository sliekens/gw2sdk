using GuildWars2.Guilds.Permissions;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Http;

internal sealed class
    GuildPermissionsByIdsRequest : IHttpRequest<Replica<HashSet<GuildPermissionSummary>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/guild/permissions") { AcceptEncoding = "gzip" };

    public GuildPermissionsByIdsRequest(IReadOnlyCollection<GuildPermission> guildPermissionIds)
    {
        Check.Collection(guildPermissionIds);
        GuildPermissionIds = guildPermissionIds;
    }

    public IReadOnlyCollection<GuildPermission> GuildPermissionIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<GuildPermissionSummary>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        {
                            "ids", GuildPermissionIds.Select(
#if NET
                                id => Enum.GetName(id)!
#else
                                id => Enum.GetName(typeof(GuildPermission), id)!
#endif
                            )
                        },
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
        return new Replica<HashSet<GuildPermissionSummary>>
        {
            Value =
                json.RootElement.GetSet(
                    entry => entry.GetGuildPermissionSummary(MissingMemberBehavior)
                ),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
