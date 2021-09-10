using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GW2SDK.Http.Caching
{
    [PublicAPI]
    public interface IHttpCacheStore
    {
        /// <summary>Enumerates all cached responses for the specified cache key.</summary>
        /// <param name="primaryKey">A cache key in the form: "{Method} {Url}".</param>
        IAsyncEnumerable<ResponseCacheEntry> GetEntriesAsync(string primaryKey);

        /// <summary>Inserts or updates a cached response.</summary>
        /// <param name="primaryKey">The cache key in the form: "{Method} {Url}".</param>
        /// <param name="entry">The response data to store.</param>
        /// <returns></returns>
        Task StoreEntryAsync(string primaryKey, ResponseCacheEntry entry);
    }
}
