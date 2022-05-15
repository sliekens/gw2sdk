using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Crafting;

namespace GW2SDK.TestDataHelper;

public class JsonRecipeService
{
    private readonly HttpClient http;

    public JsonRecipeService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<ISet<string>> GetAllJsonRecipes()
    {
        var ids = await GetRecipeIds().ConfigureAwait(false);
        var items = new SortedSet<string>();
        await foreach (var item in GetJsonRecipesByIds(ids))
        {
            items.Add(item);
        }

        return items;
    }

    private async Task<IReadOnlyCollection<int>> GetRecipeIds()
    {
        var request = new RecipesIndexRequest();
        var response = await request.SendAsync(http, CancellationToken.None);
        return response.Values;
    }

    public IAsyncEnumerable<string> GetJsonRecipesByIds(
        IReadOnlyCollection<int> itemIds,
        IProgress<ICollectionContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        var producer = SplitQuery.Create<int, string>(
            async (range, ct) =>
            {
                var request = new BulkRequest("/v2/recipes") { Ids = range };
                var json = await request.SendAsync(http, ct);
                return json.Indent(false)
                    .RootElement.EnumerateArray()
                    .Select(
                        item => item.ToString()
                            ?? throw new InvalidOperationException("Unexpected null in JSON array.")
                    )
                    .ToList();
            },
            progress
        );
        return producer.QueryAsync(itemIds, cancellationToken: cancellationToken);
    }
}
