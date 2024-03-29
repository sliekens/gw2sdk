﻿using GuildWars2.Http;

namespace GuildWars2.Hero.Equipment.MailCarriers.Http;

internal sealed class MailCarrierByIdRequest(int mailCarrierId) : IHttpRequest<MailCarrier>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/mailcarriers")
    {
        AcceptEncoding = "gzip"
    };

    public int MailCarrierId { get; } = mailCarrierId;

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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetMailCarrier(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
