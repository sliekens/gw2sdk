using System.Collections.Generic;
using System.Threading.Tasks;

namespace GW2SDK.Http
{
    public interface IHttpCacheStore
    {
        IAsyncEnumerable<ResponseCacheEntry> GetEntries(string primaryKey);

        Task StoreEntry(string primaryKey, ResponseCacheEntry entry);
    }
}