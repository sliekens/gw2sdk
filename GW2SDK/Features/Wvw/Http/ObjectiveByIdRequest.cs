using GuildWars2.Http;
using GuildWars2.Wvw.Objectives;

namespace GuildWars2.Wvw.Http;

internal sealed class ObjectiveByIdRequest : IHttpRequest<Replica<Objective>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/objectives") { AcceptEncoding = "gzip" };

    public ObjectiveByIdRequest(string objectiveId)
    {
        ObjectiveId = objectiveId;
    }

    public string ObjectiveId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Objective>> SendAsync(
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
        return new Replica<Objective>
        {
            Value = json.RootElement.GetObjective(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
