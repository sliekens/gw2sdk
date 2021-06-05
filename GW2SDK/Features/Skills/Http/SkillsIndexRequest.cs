using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Skills.Http
{
    [PublicAPI]
    public sealed class SkillsIndexRequest
    {
        public static implicit operator HttpRequestMessage(SkillsIndexRequest _)
        {
            var location = new Uri("/v2/skills", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
