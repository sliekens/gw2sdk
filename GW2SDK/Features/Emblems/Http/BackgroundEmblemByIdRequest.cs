using GuildWars2.Http;

namespace GuildWars2.Emblems.Http;

internal sealed class BackgroundEmblemByIdRequest : IHttpRequest2<Emblem>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/emblem/backgrounds") { AcceptEncoding = "gzip" };

    public BackgroundEmblemByIdRequest(int backgroundEmblemId)
    {
        BackgroundEmblemId = backgroundEmblemId;
    }

    public int BackgroundEmblemId { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Emblem Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", BackgroundEmblemId },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetEmblem(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
