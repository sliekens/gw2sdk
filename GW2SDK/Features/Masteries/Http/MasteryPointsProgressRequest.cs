using GuildWars2.Http;

namespace GuildWars2.Masteries.Http;

internal sealed class MasteryPointsProgressRequest : IHttpRequest2<MasteryPointsProgress>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/account/mastery/points")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
        };

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(MasteryPointsProgress Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        var request = Template with { BearerToken = AccessToken };
        using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetMasteryPointsProgress(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
