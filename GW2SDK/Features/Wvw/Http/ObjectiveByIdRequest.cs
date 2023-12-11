using GuildWars2.Http;
using GuildWars2.Wvw.Objectives;

namespace GuildWars2.Wvw.Http;

internal sealed class ObjectiveByIdRequest(string objectiveId) : IHttpRequest<Objective>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/objectives") { AcceptEncoding = "gzip" };

    public string ObjectiveId { get; } = objectiveId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

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
        var value = json.RootElement.GetObjective(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
