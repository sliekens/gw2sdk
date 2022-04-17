using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Professions.Json;
using GW2SDK.Professions.Models;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Professions.Http;

[PublicAPI]
public sealed class ProfessionByNameRequest : IHttpRequest<IReplica<Profession>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/professions")
    {
        AcceptEncoding = "gzip"
    };

    public ProfessionByNameRequest(ProfessionName professionName)
    {
        Check.Constant(professionName, nameof(professionName));
        ProfessionName = professionName;
    }

    public ProfessionName ProfessionName { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Profession>> SendAsync(HttpClient httpClient, CancellationToken cancellationToken)
    {
        QueryBuilder search = new();
        search.Add("id", ProfessionName.ToString());
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

        var value = ProfessionReader.Read(json.RootElement, MissingMemberBehavior);
        return new Replica<Profession>(response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified);
    }
}
