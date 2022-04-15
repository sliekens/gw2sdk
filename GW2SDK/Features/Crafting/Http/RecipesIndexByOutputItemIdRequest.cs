﻿using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Crafting.Http;

[PublicAPI]
public sealed class RecipesIndexByOutputItemIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/recipes/search")
    {
        AcceptEncoding = "gzip"
    };

    public RecipesIndexByOutputItemIdRequest(int outputItemId)
    {
        OutputItemId = outputItemId;
    }

    public int OutputItemId { get; }

    public static implicit operator HttpRequestMessage(RecipesIndexByOutputItemIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("output", r.OutputItemId);
        var request = Template with
        {
            Arguments = search
        };
        return request.Compile();
    }
}
