using GuildWars2.Http;

namespace GuildWars2.Hero.Equipment.Finishers.Http;

internal sealed class FinisherByIdRequest(int finisherId) : IHttpRequest<Finisher>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/finishers") { AcceptEncoding = "gzip" };

    public int FinisherId { get; } = finisherId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Finisher Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", FinisherId },
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
        var value = json.RootElement.GetFinisher(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
