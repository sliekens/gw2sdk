using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Professions;

[PublicAPI]
public sealed class ProfessionsByNamesRequest : IHttpRequest<IReplicaSet<Profession>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/professions")
    {
        AcceptEncoding = "gzip"
    };

    public ProfessionsByNamesRequest(IReadOnlyCollection<ProfessionName> professionNames)
    {
        Check.Collection(professionNames, nameof(professionNames));
        ProfessionNames = professionNames;
    }

    public IReadOnlyCollection<ProfessionName> ProfessionNames { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Profession>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new() { { "ids", ProfessionNames.Select(name => name.ToString()) } };
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

        var value = json.RootElement.GetSet(entry => entry.GetProfession(MissingMemberBehavior));
        return new ReplicaSet<Profession>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
