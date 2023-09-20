using GuildWars2.Crafting;

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
        var items = new SortedSet<string>();
        await foreach (var item in GetJsonRecipesByIds(ids, progress))
        {
            items.Add(item);
        }

        return items;
    }

    private async Task<HashSet<int>> GetRecipeIds() =>
        await new RecipesIndexRequest().SendAsync(http, CancellationToken.None);

    public IAsyncEnumerable<string> GetJsonRecipesByIds(
        IReadOnlyCollection<int> ids,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        return BulkQuery.QueryAsync(
            ids,
            GetChunk,
            degreeOfParalllelism: 100,
            progress: progress,
            cancellationToken: cancellationToken
        );

        async Task<IReadOnlyCollection<string>> GetChunk(IReadOnlyCollection<int> chunk, CancellationToken cancellationToken)
        {
            var request = new BulkRequest("/v2/recipes") { Ids = chunk };
            var json = await request.SendAsync(http, cancellationToken);
            return json.Indent(false)
                .RootElement.EnumerateArray()
                .Select(
                    item => item.ToString()
                        ?? throw new InvalidOperationException("Unexpected null in JSON array.")
                )
                .ToList();
        }
    }
}
