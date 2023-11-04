using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.MailCarriers.Http;

internal sealed class MailCarriersByIdsRequest : IHttpRequest<Replica<HashSet<MailCarrier>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/mailcarriers")
    {
        AcceptEncoding = "gzip"
    };

    public MailCarriersByIdsRequest(IReadOnlyCollection<int> mailCarrierIds)
    {
        Check.Collection(mailCarrierIds);
        MailCarrierIds = mailCarrierIds;
    }

    public IReadOnlyCollection<int> MailCarrierIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<MailCarrier>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", MailCarrierIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetMailCarrier(MissingMemberBehavior));
        return new Replica<HashSet<MailCarrier>>
        {
            Value = value,
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
