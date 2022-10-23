using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Stories;

[PublicAPI]
public sealed class BackstoryAnswerByIdRequest : IHttpRequest<IReplica<BackstoryAnswer>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/backstory/answers")
    {
        AcceptEncoding = "gzip"
    };

    public BackstoryAnswerByIdRequest(string answerId)
    {
        AnswerId = answerId;
    }

    public string AnswerId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<BackstoryAnswer>> SendAsync(
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
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetBackstoryAnswer(MissingMemberBehavior);
        return new Replica<BackstoryAnswer>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
