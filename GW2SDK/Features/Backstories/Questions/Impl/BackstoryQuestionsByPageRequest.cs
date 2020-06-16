using System;
using System.Net.Http;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Backstories.Questions.Impl
{
    public sealed class BackstoryQuestionsByPageRequest
    {
        public BackstoryQuestionsByPageRequest(int pageIndex, int? pageSize = null)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int PageIndex { get; }

        public int? PageSize { get; }

        public static implicit operator HttpRequestMessage(BackstoryQuestionsByPageRequest r)
        {
            var search = new QueryBuilder();
            search.Add("page", r.PageIndex);
            if (r.PageSize.HasValue) search.Add("page_size", r.PageSize.Value);
            var location = new Uri($"/v2/backstory/questions?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
