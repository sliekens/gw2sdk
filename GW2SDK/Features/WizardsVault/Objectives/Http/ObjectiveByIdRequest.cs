using GuildWars2.Http;

namespace GuildWars2.WizardsVault.Objectives.Http;

internal sealed class ObjectiveByIdRequest(int objectiveId) : IHttpRequest<Objective>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wizardsvault/objectives") { AcceptEncoding = "gzip" };

    public int ObjectiveId { get; } = objectiveId;

    public Language? Language { get; init; }

    
    public async Task<(Objective Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", ObjectiveId },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetObjective();
        return (value, new MessageContext(response));
    }
}
