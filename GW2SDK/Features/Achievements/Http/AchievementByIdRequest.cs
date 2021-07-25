using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Http
{
    [PublicAPI]
    public sealed class AchievementByIdRequest
    {
        public AchievementByIdRequest(int achievementId, Language? language)
        {
            AchievementId = achievementId;
            Language = language;
        }

        public int AchievementId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(AchievementByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.AchievementId);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/achievements?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
