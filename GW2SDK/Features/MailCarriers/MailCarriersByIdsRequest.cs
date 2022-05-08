using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.MailCarriers;

[PublicAPI]
public sealed class MailCarriersByIdsRequest : IHttpRequest<IReplicaSet<MailCarrier>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/mailcarriers")
    {
        AcceptEncoding = "gzip"
    };

    public MailCarriersByIdsRequest(IReadOnlyCollection<int> mailCarrierIds)
    {
        Check.Collection(mailCarrierIds, nameof(mailCarrierIds));
        MailCarrierIds = mailCarrierIds;
    }

    public IReadOnlyCollection<int> MailCarrierIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<MailCarrier>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new() { { "ids", MailCarrierIds } };
        var request = Template with
        {
            Arguments = search,
            AcceptLanguage = Language?.Alpha2Code
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

        var value = json.RootElement.GetSet(entry => entry.GetMailCarrier(MissingMemberBehavior));
        return new ReplicaSet<MailCarrier>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
