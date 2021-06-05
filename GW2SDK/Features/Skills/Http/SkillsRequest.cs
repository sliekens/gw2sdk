using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Skills.Http
{
    [PublicAPI]
    public sealed class SkillsRequest
    {
        public static implicit operator HttpRequestMessage(SkillsRequest _)
        {
            var location = new Uri("/v2/skills?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
