using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Colors;

[PublicAPI]
public sealed class ColorsByIdsRequest : IHttpRequest<IReplicaSet<Dye>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/colors")
    {
        AcceptEncoding = "gzip"
    };

    public ColorsByIdsRequest(IReadOnlyCollection<int> colorIds)
    {
        Check.Collection(colorIds, nameof(colorIds));
        ColorIds = colorIds;
    }

    public IReadOnlyCollection<int> ColorIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Dye>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("ids", ColorIds);
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

        var value = json.RootElement.GetSet(entry => entry.GetDye(MissingMemberBehavior));
        return new ReplicaSet<Dye>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
