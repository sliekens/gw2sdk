using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GW2SDK.Http.Caching
{
    [PublicAPI]
    public sealed class InMemoryHttpCacheStore : IHttpCacheStore
    {
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<Guid, ResponseCacheEntry>> responseCache = new();

        public async IAsyncEnumerable<ResponseCacheEntry> GetEntriesAsync(string primaryKey)
        {
            if (responseCache.TryGetValue(primaryKey, out var entries))
            {
                foreach (var entry in entries.Values)
                {
                    yield return entry;
                }
            }

            await Task.CompletedTask.ConfigureAwait(false);
        }

        public Task StoreEntryAsync(string primaryKey, ResponseCacheEntry entry, CancellationToken cancellationToken)
        {
            if (!responseCache.TryGetValue(primaryKey, out var entries))
            {
                responseCache[primaryKey] = entries = new ConcurrentDictionary<Guid, ResponseCacheEntry>();
            }

            if (entry.Id == default)
            {
                entry.Id = Guid.NewGuid();
            }

            entries[entry.Id] = entry;

            return Task.CompletedTask;
        }
    }
}