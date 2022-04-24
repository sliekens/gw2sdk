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
        QueryBuilder search = new();
        search.Add("id", QuestionId);
        var request = Template with
        {
            Arguments = search,
            AcceptLanguage = Language?.Alpha2Code
        };

        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
                )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = BackstoryQuestionReader.Read(json.RootElement, MissingMemberBehavior);
        return new Replica<BackstoryQuestion>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
