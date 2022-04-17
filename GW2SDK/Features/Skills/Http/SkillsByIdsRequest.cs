using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Skills.Json;
using GW2SDK.Skills.Models;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Skills.Http;

[PublicAPI]
public sealed class SkillsByIdsRequest : IHttpRequest<IReplicaSet<Skill>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/skills")
    {
        AcceptEncoding = "gzip"
    };

    public SkillsByIdsRequest(IReadOnlyCollection<int> skillIds)
    {
        Check.Collection(skillIds, nameof(skillIds));
        SkillIds = skillIds;
    }

    public IReadOnlyCollection<int> SkillIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Skill>> SendAsync(HttpClient httpClient, CancellationToken cancellationToken)
    {
        QueryBuilder search = new();
        search.Add("ids", SkillIds);
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

        var value = json.RootElement.GetSet(entry => SkillReader.Read(entry, MissingMemberBehavior));
        return new ReplicaSet<Skill>(response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified);
    }
}
