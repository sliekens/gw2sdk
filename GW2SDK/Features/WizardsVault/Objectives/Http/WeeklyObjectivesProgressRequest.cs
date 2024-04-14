using GuildWars2.Http;

namespace GuildWars2.WizardsVault.Objectives.Http;

internal sealed class WeeklyObjectivesProgressRequest : IHttpRequest<WeeklyObjectivesProgress>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/account/wizardsvault/weekly")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
        };

    public required string? AccessToken { get; init; }

    
    public async Task<(WeeklyObjectivesProgress Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetWeeklyObjectivesProgress();
        return (value, new MessageContext(response));
    }
}
