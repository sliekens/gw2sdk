using GuildWars2.Guilds.Permissions;
using GuildWars2.Http;

namespace GuildWars2.Guilds.Http;

internal sealed class GuildPermissionByIdRequest(string guildPermissionId)
    : IHttpRequest<GuildPermissionSummary>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/guild/permissions") { AcceptEncoding = "gzip" };

    public string GuildPermissionId { get; } = guildPermissionId;

    public Language? Language { get; init; }

    
    public async Task<(GuildPermissionSummary Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", GuildPermissionId },
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
        var value = json.RootElement.GetGuildPermissionSummary();
        return (value, new MessageContext(response));
    }
}
