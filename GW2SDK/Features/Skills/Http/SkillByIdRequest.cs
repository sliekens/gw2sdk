using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Skills.Http
{
    [PublicAPI]
    public sealed class SkillByIdRequest
    {
        public SkillByIdRequest(int skillId)
        {
            SkillId = skillId;
        }

        public int SkillId { get; }

        public static implicit operator HttpRequestMessage(SkillByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.SkillId);
            var location = new Uri($"/v2/skills?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
