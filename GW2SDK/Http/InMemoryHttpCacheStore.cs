using System.Collections.Generic;
using System.Threading.Tasks;

namespace GW2SDK.Http
{
    public sealed class InMemoryHttpCacheStore : IHttpCacheStore
    {
        private readonly Dictionary<string, List<ResponseCacheEntry>> responseCache = new();

        public async IAsyncEnumerable<ResponseCacheEntry> GetEntries(string primaryKey)
        {
            if (!responseCache.TryGetValue(primaryKey, out var cacheEntries))
            {
                yield break;
            }

            foreach (var entry in cacheEntries)
            {
                yield return entry;
            }

            await Task.CompletedTask.ConfigureAwait(false);
        }

        public Task StoreEntry(string primaryKey, ResponseCacheEntry entry)
        {
            if (!responseCache.TryGetValue(primaryKey, out var entries))
            {
                responseCache[primaryKey] = entries = new List<ResponseCacheEntry>();
            }

            entries.Add(entry);
            return Task.CompletedTask;
        }
    }
}