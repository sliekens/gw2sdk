using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Groups.Http
{
    [PublicAPI]
    public sealed class AchievementGroupsByPageRequest
    {
        public AchievementGroupsByPageRequest(
            int pageIndex,
            int? pageSize,
            Language? language
        )
        {
            PageIndex = pageIndex;
            Language = language;
            PageSize = pageSize;
        }

        public int PageIndex { get; }

        public int? PageSize { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(AchievementGroupsByPageRequest r)
        {
            var search = new QueryBuilder();
            search.Add("page", r.PageIndex);
            if (r.PageSize.HasValue) search.Add("page_size", r.PageSize.Value);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/achievements/groups?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
