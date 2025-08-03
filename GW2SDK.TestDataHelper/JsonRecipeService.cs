using GuildWars2.Hero.Crafting.Recipes;

namespace GuildWars2.TestDataHelper;

internal sealed class JsonRecipeService(HttpClient http)
{
    public async Task<ISet<string>> GetAllJsonRecipes(IProgress<BulkProgress> progress)
    {
        var ids = await GetRecipeIds().ConfigureAwait(false);
        var entries = new SortedDictionary<int, string>();
        await foreach (var (id, entry) in GetJsonRecipesByIds(ids, progress).ConfigureAwait(false))
        {
            entries[id] = entry;
        }

        return entries.Values.ToHashSet();
    }

    private async Task<HashSet<int>> GetRecipeIds()
    {
        var recipes = new RecipesClient(http);
        var (ids, _) = await recipes.GetRecipesIndex().ConfigureAwait(false);
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
            Uri resource = new Uri("/v2/recipes", UriKind.Relative);
            var request = new BulkRequest(resource) { Ids = [.. chunk] };
            var json = await request.SendAsync(http, cancellationToken).ConfigureAwait(false);
            return [.. json.RootElement.EnumerateArray().Select(item => (item.GetProperty("id").GetInt32(), item.ToJsonLine()))];
        }
    }
}
