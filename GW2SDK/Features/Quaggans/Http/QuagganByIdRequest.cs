using GuildWars2.Http;

namespace GuildWars2.Quaggans.Http;

internal sealed class QuagganByIdRequest(string quagganId) : IHttpRequest<Quaggan>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/quaggans")
    {
        AcceptEncoding = "gzip"
    };

    public string QuagganId { get; } = quagganId;

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Quaggan Value, MessageContext Context)> SendAsync(
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetQuaggan(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
