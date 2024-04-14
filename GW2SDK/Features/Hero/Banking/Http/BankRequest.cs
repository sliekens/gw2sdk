﻿using GuildWars2.Http;

namespace GuildWars2.Hero.Banking.Http;

internal sealed class BankRequest : IHttpRequest<Bank>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/account/bank")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public required string? AccessToken { get; init; }

    
    public async Task<(Bank Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with { BearerToken = AccessToken },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetBank();
        return (value, new MessageContext(response));
    }
}
