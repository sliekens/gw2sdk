using GuildWars2.Http;

namespace GuildWars2.Quaggans.Http;

internal sealed class QuagganByIdRequest : IHttpRequest<Replica<Quaggan>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/quaggans")
    {
        AcceptEncoding = "gzip"
    };

    public QuagganByIdRequest(string quagganId)
    {
        QuagganId = quagganId;
    }

    public string QuagganId { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Quaggan>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", QuagganId },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetQuaggan(MissingMemberBehavior);
        return new Replica<Quaggan>
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
