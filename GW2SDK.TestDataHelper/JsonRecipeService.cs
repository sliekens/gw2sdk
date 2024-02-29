﻿using GuildWars2.Hero.Crafting.Recipes;

namespace GuildWars2.TestDataHelper;

public class JsonRecipeService(HttpClient http)
{
    public async Task<ISet<string>> GetAllJsonRecipes(IProgress<BulkProgress> progress)
    {
        var ids = await GetRecipeIds().ConfigureAwait(false);
        var entries = new SortedDictionary<int, string>();
        await foreach (var (id, entry) in GetJsonRecipesByIds(ids, progress))
        {
            entries[id] = entry;
        }

        return entries.Values.ToHashSet();
    }

    private async Task<HashSet<int>> GetRecipeIds()
    {
        var recipes = new RecipesClient(http);
        var (ids, _) = await recipes.GetRecipesIndex();
        return ids;
    }

    public IAsyncEnumerable<(int, string)> GetJsonRecipesByIds(
        IReadOnlyCollection<int> ids,
        IProgress<BulkProgress>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        return BulkQuery.QueryAsync(
            ids,
            GetChunk,
            100,
            progress: progress,
            cancellationToken: cancellationToken
        );

        async Task<IReadOnlyCollection<(int, string)>> GetChunk(
            IReadOnlyCollection<int> chunk,
            CancellationToken cancellationToken
        )
        {
            var request = new BulkRequest("/v2/recipes") { Ids = chunk };
            var json = await request.SendAsync(http, cancellationToken);
            return json.Indent(false)
                .RootElement.EnumerateArray()
                .Select(item => (item.GetProperty("id").GetInt32(), item.ToString()))
                .ToList();
        }
    }
}
