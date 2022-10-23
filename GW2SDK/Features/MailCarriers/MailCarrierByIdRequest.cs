using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.MailCarriers;

[PublicAPI]
public sealed class MailCarrierByIdRequest : IHttpRequest<IReplica<MailCarrier>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/mailcarriers")
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

    public async Task<IReplica<MailCarrier>> SendAsync(
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
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetMailCarrier(MissingMemberBehavior);
        return new Replica<MailCarrier>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
