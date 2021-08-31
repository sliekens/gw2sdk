using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Skills.Http
{
    [PublicAPI]
    public sealed class SkillsByIdsRequest
    {
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
            var search = new QueryBuilder();
            search.Add("ids", r.SkillIds);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/skills?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
