using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GW2SDK.Http
{
    [PublicAPI]
    public interface IHttpCacheStore
    {
        IAsyncEnumerable<ResponseCacheEntry> GetEntries(string primaryKey);

        Task StoreEntry(string primaryKey, ResponseCacheEntry entry);
    }
}