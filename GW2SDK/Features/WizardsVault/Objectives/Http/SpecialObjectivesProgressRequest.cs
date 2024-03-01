using GuildWars2.Http;

namespace GuildWars2.WizardsVault.Objectives.Http;

internal sealed class SpecialObjectivesProgressRequest : IHttpRequest<SpecialObjectivesProgress>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/account/wizardsvault/special")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
        };

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(SpecialObjectivesProgress Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetSpecialObjectivesProgress(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
