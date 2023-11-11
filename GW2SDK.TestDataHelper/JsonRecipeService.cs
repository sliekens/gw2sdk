using GuildWars2.Hero.Crafting;

namespace GuildWars2.TestDataHelper;

public class JsonRecipeService
{
    private readonly HttpClient http;

    public JsonRecipeService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<ISet<string>> GetAllJsonRecipes(IProgress<ResultContext> progress)
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
        var crafting = new CraftingClient(http);
        var (ids, _) = await crafting.GetRecipesIndex();
        return ids;
    }

    public IAsyncEnumerable<(int, string)> GetJsonRecipesByIds(
        IReadOnlyCollection<int> ids,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        return BulkQuery.QueryAsync(
            ids,
            GetChunk,
            degreeOfParallelism: 100,
            progress: progress,
            cancellationToken: cancellationToken
        );

        async Task<IReadOnlyCollection<(int, string)>> GetChunk(IReadOnlyCollection<int> chunk, CancellationToken cancellationToken)
        {
            var request = new BulkRequest("/v2/recipes") { Ids = chunk };
            var json = await request.SendAsync(http, cancellationToken);
            return json.Indent(false)
                .RootElement.EnumerateArray()
                .Select(
                    item => (item.GetProperty("id").GetInt32(), item.ToString())
                )
                .ToList();
        }
    }
}
