using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Masteries;

[PublicAPI]
public sealed class
    MasteryProgressRequest : IHttpRequest<IReplica<IReadOnlyCollection<MasteryProgress>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "/v2/account/masteries") { AcceptEncoding = "gzip" };

    public string? AccessToken { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<IReadOnlyCollection<MasteryProgress>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        var request = Template with { BearerToken = AccessToken };
        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
                )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(
            entry => MasteryProgressReader.Read(entry, MissingMemberBehavior)
            );
        return new Replica<IReadOnlyCollection<MasteryProgress>>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
