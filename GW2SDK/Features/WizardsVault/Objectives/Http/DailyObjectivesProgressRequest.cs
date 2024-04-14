using GuildWars2.Http;

namespace GuildWars2.WizardsVault.Objectives.Http;

internal sealed class DailyObjectivesProgressRequest : IHttpRequest<DailyObjectivesProgress>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/account/wizardsvault/daily")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
        };

    public required string? AccessToken { get; init; }

    
    public async Task<(DailyObjectivesProgress Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with { BearerToken = AccessToken },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetDailyObjectivesProgress();
        return (value, new MessageContext(response));
    }
}
