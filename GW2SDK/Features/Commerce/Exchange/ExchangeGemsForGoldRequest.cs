﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Commerce.Exchange;

[PublicAPI]
public sealed class ExchangeGemsForGoldRequest : IHttpRequest<IReplica<GemsForGoldExchange>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/commerce/exchange/gems") { AcceptEncoding = "gzip" };

    public ExchangeGemsForGoldRequest(int gemsCount)
    {
        GemsCount = gemsCount;
    }

    public int GemsCount { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<GemsForGoldExchange>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "quantity", GemsCount },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        return new Replica<GemsForGoldExchange>
        {
            Value = json.RootElement.GetGemsForGoldExchange(MissingMemberBehavior),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
