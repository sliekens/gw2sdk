using GuildWars2.Skins.Http;

namespace GuildWars2.TestDataHelper;

public class JsonSkinService
{
    private readonly HttpClient http;

    public JsonSkinService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<ISet<string>> GetAllJsonSkins(IProgress<ResultContext> progress)
    {
        var ids = await GetSkinIds().ConfigureAwait(false);
        var entries = new SortedDictionary<int, string>();
        await foreach (var (id, entry) in GetJsonSkinsByIds(ids, progress))
        {
            entries[id] = entry;
        }

        return entries.Values.ToHashSet();
    }

    private async Task<HashSet<int>> GetSkinIds() =>
        await new SkinsIndexRequest().SendAsync(http, CancellationToken.None);

    public IAsyncEnumerable<(int, string)> GetJsonSkinsByIds(
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
            var request = new BulkRequest("/v2/skins") { Ids = chunk };
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
