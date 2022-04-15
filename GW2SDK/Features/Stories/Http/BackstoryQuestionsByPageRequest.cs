﻿using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Stories.Http;

[PublicAPI]
public sealed class BackstoryQuestionsByPageRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/backstory/questions")
    {
        AcceptEncoding = "gzip"
    };

    public BackstoryQuestionsByPageRequest(
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

    public static implicit operator HttpRequestMessage(BackstoryQuestionsByPageRequest r)
    {
        QueryBuilder search = new();
        search.Add("page", r.PageIndex);
        if (r.PageSize.HasValue)
        {
            search.Add("page_size", r.PageSize.Value);
        }

        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
