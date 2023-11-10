using GuildWars2.Http;

namespace GuildWars2.Hero.Equipment.Miniatures.Http;

internal sealed class MinipetByIdRequest : IHttpRequest<Minipet>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/minis") { AcceptEncoding = "gzip" };

    public MinipetByIdRequest(int minipetId)
    {
        MinipetId = minipetId;
    }

    public int MinipetId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Minipet Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", MinipetId },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetMinipet(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
