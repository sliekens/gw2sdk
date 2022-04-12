using System.Collections.Generic;
using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Skills.Http;

[PublicAPI]
public sealed class SkillsByIdsRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/skills")
    {
        AcceptEncoding = "gzip"
    };

    public SkillsByIdsRequest(IReadOnlyCollection<int> skillIds, Language? language)
    {
        Check.Collection(skillIds, nameof(skillIds));
        SkillIds = skillIds;
        Language = language;
    }

    public IReadOnlyCollection<int> SkillIds { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(SkillsByIdsRequest r)
    {
        QueryBuilder search = new();
        search.Add("ids", r.SkillIds);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}