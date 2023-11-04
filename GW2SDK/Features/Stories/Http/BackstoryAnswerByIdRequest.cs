using GuildWars2.Http;

namespace GuildWars2.Stories.Http;

internal sealed class BackstoryAnswerByIdRequest : IHttpRequest<BackstoryAnswer>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/backstory/answers")
    {
        AcceptEncoding = "gzip"
    };

    public BackstoryAnswerByIdRequest(string answerId)
    {
        AnswerId = answerId;
    }

    public string AnswerId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(BackstoryAnswer Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new()
        {
            { "id", AnswerId },
            { "v", SchemaVersion.Recommended }
        };

        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = search,
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetBackstoryAnswer(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
