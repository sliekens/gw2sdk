using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Skills.Http;

[PublicAPI]
public sealed class SkillByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/skills")
    {
        AcceptEncoding = "gzip"
    };

    public SkillByIdRequest(int skillId, Language? language)
    {
        SkillId = skillId;
        Language = language;
    }

    public int SkillId { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(SkillByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.SkillId);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
