using GuildWars2.Guilds.Logs;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Http;

internal sealed class GuildLogRequest : IHttpRequest<List<GuildLogEntry>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/guild/:id/log") { AcceptEncoding = "gzip" };

    public GuildLogRequest(string id)
    {
        Id = id;
    }

    public string Id { get; }

    public int? Since { get; init; }

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(List<GuildLogEntry> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        var arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } };
        if (Since.HasValue)
        {
            arguments.Add("since", Since.Value);
        }

        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(":id", Id),
                    Arguments = arguments,
                    BearerToken = AccessToken
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value =
            json.RootElement.GetList(entry => entry.GetGuildLogEntry(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
