using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Stories;

[PublicAPI]
public sealed class BackstoryQuestionByIdRequest : IHttpRequest<IReplica<BackstoryQuestion>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/backstory/questions") { AcceptEncoding = "gzip" };

    public BackstoryQuestionByIdRequest(int questionId)
    {
        QuestionId = questionId;
    }

    public int QuestionId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<BackstoryQuestion>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", QuestionId },
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

        return new Replica<BackstoryQuestion>
        {
            Value = json.RootElement.GetBackstoryQuestion(MissingMemberBehavior),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
