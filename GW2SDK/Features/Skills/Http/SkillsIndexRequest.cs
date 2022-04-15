using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Skills.Http;

[PublicAPI]
public sealed class SkillsIndexRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/skills")
    {
        AcceptEncoding = "gzip"
    };

    public static implicit operator HttpRequestMessage(SkillsIndexRequest _) => Template.Compile();
}
