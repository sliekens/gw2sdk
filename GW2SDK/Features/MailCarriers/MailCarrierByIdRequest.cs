﻿using GuildWars2.Http;

namespace GuildWars2.MailCarriers;

[PublicAPI]
public sealed class MailCarrierByIdRequest : IHttpRequest<Replica<MailCarrier>>
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

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<MailCarrier>> SendAsync(
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
        return new Replica<MailCarrier>
        {
            Value = json.RootElement.GetMailCarrier(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
