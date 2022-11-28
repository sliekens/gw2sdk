﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Commerce.Exchange;

[PublicAPI]
public sealed class ExchangeGoldForGemsRequest : IHttpRequest<IReplica<GoldForGemsExchange>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/commerce/exchange/coins") { AcceptEncoding = "gzip" };

    public ExchangeGoldForGemsRequest(int coinsCount)
    {
        CoinsCount = coinsCount;
    }

    public int CoinsCount { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<GoldForGemsExchange>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "quantity", CoinsCount },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetGoldForGemsExchange(MissingMemberBehavior);
        return new Replica<GoldForGemsExchange>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
