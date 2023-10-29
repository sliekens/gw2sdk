﻿using GuildWars2.Http;

namespace GuildWars2.Crafting.Http;

internal sealed class RecipeByIdRequest : IHttpRequest<Replica<Recipe>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/recipes")
    {
        AcceptEncoding = "gzip"
    };

    public RecipeByIdRequest(int recipeId)
    {
        RecipeId = recipeId;
    }

    public int RecipeId { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Recipe>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", RecipeId },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<Recipe>
        {
            Value = json.RootElement.GetRecipe(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
