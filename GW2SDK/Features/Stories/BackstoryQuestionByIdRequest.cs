using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Stories;

[PublicAPI]
public sealed class BackstoryQuestionByIdRequest : IHttpRequest<IReplica<BackstoryQuestion>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "/v2/backstory/questions") { AcceptEncoding = "gzip" };

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
                    Arguments = new QueryBuilder { { "id", QuestionId } },
                    AcceptLanguage = Language?.Alpha2Code
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetBackstoryQuestion(MissingMemberBehavior);
        return new Replica<BackstoryQuestion>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
