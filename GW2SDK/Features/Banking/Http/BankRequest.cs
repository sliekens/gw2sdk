﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Banking.Json;
using GW2SDK.Banking.Models;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Banking.Http;

[PublicAPI]
public sealed class BankRequest : IHttpRequest<IReplica<AccountBank>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/account/bank")
    {
        AcceptEncoding = "gzip"
    };

    public string? AccessToken { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<AccountBank>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        var request = Template with
        {
            Arguments = search,
            BearerToken = AccessToken
        };

        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
                )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = AccountBankReader.Read(json.RootElement, MissingMemberBehavior);
        return new Replica<AccountBank>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
