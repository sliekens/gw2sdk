using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Skins.Http;

namespace GW2SDK.TestDataHelper;

public class JsonSkinService
{
    private readonly HttpClient http;

    public JsonSkinService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<ISet<string>> GetAllJsonSkins()
    {
        var ids = await GetSkinIds().ConfigureAwait(false);

        var batches = new Queue<IEnumerable<int>>(ids.Buffer(200));

        var result = new List<string>();
        var work = new List<Task<List<string>>>();

        for (var i = 0; i < 12; i++)
        {
            if (!batches.TryDequeue(out var batch))
            {
                break;
            }

            work.Add(GetJsonSkinsByIds(batch.ToList()));
        }

        while (work.Count > 0)
        {
            var done = await Task.WhenAny(work);
            result.AddRange(done.Result);

            work.Remove(done);

            if (batches.TryDequeue(out var batch))
            {
                work.Add(GetJsonSkinsByIds(batch.ToList()));
            }
        }

        return new SortedSet<string>(result, StringComparer.Ordinal);
    }

    private async Task<IReadOnlyCollection<int>> GetSkinIds()
    {
        var request = new SkinsIndexRequest();
        var response = await request.SendAsync(http, CancellationToken.None);
        return response.Values;
    }

    private async Task<List<string>> GetJsonSkinsByIds(IReadOnlyCollection<int> skinIds)
    {
        var request = new BulkRequest("/v2/skins", skinIds);
        var json = await request.SendAsync(http, CancellationToken.None).ConfigureAwait(false);
        return json.Indent(false)
            .RootElement.EnumerateArray()
            .Select(
                item => item.ToString()
                    ?? throw new InvalidOperationException("Unexpected null in JSON array.")
                )
            .ToList();
    }
}
