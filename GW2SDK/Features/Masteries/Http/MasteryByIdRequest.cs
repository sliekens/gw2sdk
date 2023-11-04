using GuildWars2.Http;

namespace GuildWars2.Masteries.Http;

internal sealed class MasteryByIdRequest : IHttpRequest<Replica<Mastery>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/masteries")
    {
        AcceptEncoding = "gzip"
    };

    public MasteryByIdRequest(int masteryId)
    {
        MasteryId = masteryId;
    }

    public int MasteryId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Mastery>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", MasteryId },
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
        var value = json.RootElement.GetMastery(MissingMemberBehavior);
        return new Replica<Mastery>
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
