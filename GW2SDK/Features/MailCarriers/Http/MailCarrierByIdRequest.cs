using GuildWars2.Http;

namespace GuildWars2.MailCarriers.Http;

internal sealed class MailCarrierByIdRequest : IHttpRequest<MailCarrier>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/mailcarriers")
    {
        AcceptEncoding = "gzip"
    };

    public MailCarrierByIdRequest(int mailCarrierId)
    {
        MailCarrierId = mailCarrierId;
    }

    public int MailCarrierId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(MailCarrier Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", MailCarrierId },
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
        var value = json.RootElement.GetMailCarrier(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
