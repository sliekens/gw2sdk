using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Guilds.Permissions;

[PublicAPI]
public sealed class GuildPermissionByIdRequest : IHttpRequest<IReplica<GuildPermissionSummary>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/guild/permissions") { AcceptEncoding = "gzip" };

    public GuildPermissionByIdRequest(GuildPermission guildPermissionId)
    {
        GuildPermissionId = guildPermissionId;
    }

    public GuildPermission GuildPermissionId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<GuildPermissionSummary>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", GuildPermissionId.ToString() },
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

        var value = json.RootElement.GetGuildPermissionSummary(MissingMemberBehavior);
        return new Replica<GuildPermissionSummary>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
