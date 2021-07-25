﻿using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Categories.Http
{
    [PublicAPI]
    public sealed class AchievementCategoriesByPageRequest
    {
        public AchievementCategoriesByPageRequest(
            int pageIndex,
            int? pageSize,
            Language? language
        )
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Language = language;
        }

        public int PageIndex { get; }

        public int? PageSize { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(AchievementCategoriesByPageRequest r)
        {
            var search = new QueryBuilder();
            search.Add("page", r.PageIndex);
            if (r.PageSize.HasValue) search.Add("page_size", r.PageSize.Value);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/achievements/categories?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
