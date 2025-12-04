using System.Text.Json;

using GuildWars2.Hero.Crafting.Recipes;

namespace GuildWars2.TestDataHelper;

internal sealed class JsonRecipeService(HttpClient http)
{
    public async Task<ISet<string>> GetAllJsonRecipes(IProgress<BulkProgress> progress)
    {
        HashSet<int> ids = await GetRecipeIds().ConfigureAwait(false);
        SortedDictionary<int, string> entries = [];
        await foreach ((int id, string entry) in GetJsonRecipesByIds(ids, progress).ConfigureAwait(false))
        {
            entries[id] = entry;
        }

        return entries.Values.ToHashSet();
    }

    private async Task<HashSet<int>> GetRecipeIds()
    {
        RecipesClient recipes = new(http);
        (HashSet<int> ids, _) = await recipes.GetRecipesIndex().ConfigureAwait(false);
        return ids;
    }

    public IAsyncEnumerable<(int, string)> GetJsonRecipesByIds(
        IEnumerable<int> ids,
        IProgress<BulkProgress>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        return BulkQuery.QueryAsync(
            ids,
            GetChunk,
            progress: progress,
            cancellationToken: cancellationToken
        );

        async Task<IReadOnlyCollection<(int, string)>> GetChunk(
            IEnumerable<int> chunk,
            CancellationToken cancellationToken
        )
        {
            Uri resource = new("/v2/recipes", UriKind.Relative);
            BulkRequest request = new(resource) { Ids = [.. chunk] };
            JsonDocument json = await request.SendAsync(http, cancellationToken).ConfigureAwait(false);
            return [.. json.RootElement.EnumerateArray().Select(item => (item.GetProperty("id").GetInt32(), item.ToJsonLine()))];
        }
    }
}
