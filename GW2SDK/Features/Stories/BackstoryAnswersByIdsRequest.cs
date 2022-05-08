using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Stories;

[PublicAPI]
public sealed class BackstoryAnswersByIdsRequest : IHttpRequest<IReplicaSet<BackstoryAnswer>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/backstory/answers")
    {
        AcceptEncoding = "gzip"
    };

    public BackstoryAnswersByIdsRequest(IReadOnlyCollection<string> answerIds)
    {
        Check.Collection(answerIds, nameof(answerIds));
        AnswerIds = answerIds;
    }

    public IReadOnlyCollection<string> AnswerIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<BackstoryAnswer>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder { { "ids", AnswerIds } },
                    AcceptLanguage = Language?.Alpha2Code
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(
            entry => entry.GetBackstoryAnswer(MissingMemberBehavior)
        );
        return new ReplicaSet<BackstoryAnswer>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
