using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Http
{
    [PublicAPI]
    public sealed class AchievementsByIdsRequest
    {
        public AchievementsByIdsRequest(IReadOnlyCollection<int> achievementIds, Language? language)
        {
            if (achievementIds is null)
            {
                throw new ArgumentNullException(nameof(achievementIds));
            }

            if (achievementIds.Count == 0)
            {
                throw new ArgumentException("Achievement IDs cannot be an empty collection.", nameof(achievementIds));
            }

            AchievementIds = achievementIds;
            Language = language;
        }

        public IReadOnlyCollection<int> AchievementIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(AchievementsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.AchievementIds);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/achievements?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
