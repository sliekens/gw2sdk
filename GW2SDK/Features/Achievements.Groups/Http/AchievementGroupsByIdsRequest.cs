using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Groups.Http
{
    [PublicAPI]
    public sealed class AchievementGroupsByIdsRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/achievements/groups")
        {
            AcceptEncoding = "gzip"
        };

        public AchievementGroupsByIdsRequest(IReadOnlyCollection<string> achievementGroupIds, Language? language)
        {
            Check.Collection(achievementGroupIds, nameof(achievementGroupIds));
            AchievementGroupIds = achievementGroupIds;
            Language = language;
        }

        public IReadOnlyCollection<string> AchievementGroupIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(AchievementGroupsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.AchievementGroupIds);
            var request = Template with
            {
                AcceptLanguage = r.Language?.Alpha2Code,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
