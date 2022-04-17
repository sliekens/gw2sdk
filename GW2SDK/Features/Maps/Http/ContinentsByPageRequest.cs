using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Maps.Json;
using GW2SDK.Maps.Models;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Maps.Http;

[PublicAPI]
public sealed class ContinentsByPageRequest : IHttpRequest<IReplicaPage<Continent>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/continents")
    {
        AcceptEncoding = "gzip"
    };

    public ContinentsByPageRequest(int pageIndex)
    {
        PageIndex = pageIndex;
    }

    public int PageIndex { get; }

    public int? PageSize { get; init; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaPage<Continent>> SendAsync(HttpClient httpClient, CancellationToken cancellationToken)
    {
        QueryBuilder search = new();
        search.Add("page", PageIndex);
        if (PageSize.HasValue)
        {
            search.Add("page_size", PageSize.Value);
        }

        var request = Template with
        {
            Arguments = search,
            AcceptLanguage = Language?.Alpha2Code
        };

        using var response = await httpClient.SendAsync(request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken)
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken)
            .ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => ContinentReader.Read(entry, MissingMemberBehavior));
        return new ReplicaPage<Continent>(response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetPageContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified);
    }
}
