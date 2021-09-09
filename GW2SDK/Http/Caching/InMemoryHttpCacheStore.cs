using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GW2SDK.Http.Caching
{
    [PublicAPI]
    public sealed class InMemoryHttpCacheStore : IHttpCacheStore
    {
        private readonly ConcurrentDictionary<string, ResponseCacheEntry> responseCache = new();

        public async IAsyncEnumerable<ResponseCacheEntry> GetEntries(string primaryKey)
        {
            if (responseCache.TryGetValue(primaryKey, out var entry))
            {
                yield return entry;
            }

            await Task.CompletedTask.ConfigureAwait(false);
        }

        public Task StoreEntry(string primaryKey, ResponseCacheEntry entry)
        {
            responseCache[primaryKey] = entry;

            return Task.CompletedTask;
        }
    }
}