using GuildWars2.Http;

namespace GuildWars2.Hero.Builds.Http;

internal sealed class StoredBuildRequest(int slotNumber) : IHttpRequest<Build>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/account/buildstorage") { AcceptEncoding = "gzip" };

    public int SlotNumber { get; } = slotNumber;

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Build Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", SlotNumber },
                        { "v", SchemaVersion.Recommended }
                    },
                    BearerToken = AccessToken
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetBuild(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
