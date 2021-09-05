using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Http;
using StackExchange.Redis;

namespace GW2SDK.Tests.TestInfrastructure
{
    public class RedisHttpCacheStore : IHttpCacheStore
    {
        private static readonly string RedisKeyPrefix = typeof(RedisHttpCacheStore).AssemblyQualifiedName;

        private readonly IConnectionMultiplexer redis;

        public RedisHttpCacheStore(IConnectionMultiplexer redis)
        {
            this.redis = redis ?? throw new ArgumentNullException(nameof(redis));
        }

        public async IAsyncEnumerable<ResponseCacheEntry> GetEntries(string primaryKey)
        {
            var db = redis.GetDatabase();

            var redisKey = new RedisKey($"[{RedisKeyPrefix}] {primaryKey}");
            foreach (var hashEntry in await db.StreamRangeAsync(redisKey))
            {
#if NETCOREAPP3_1_OR_GREATER
                var values = hashEntry.Values.Select(entry =>
                    KeyValuePair.Create<string, string>(entry.Name.ToString(), entry.Value.ToString()));
#else
                var values = hashEntry.Values
                    .Select(entry => new KeyValuePair<string, string>(entry.Name.ToString(), entry.Value.ToString()));
#endif
                var responseCacheEntry = ResponseCacheEntry.Hydrate(values);
                yield return responseCacheEntry;
            }
        }

        public async Task StoreEntry(string primaryKey, ResponseCacheEntry entry)
        {
            var db = redis.GetDatabase();

            var redisKey = new RedisKey($"[{RedisKeyPrefix}] {primaryKey}");

            await db.StreamAddAsync(redisKey,
                entry.Serialize()
                    .Select(kvp => new NameValueEntry(new RedisValue(kvp.Key), new RedisValue(kvp.Value)))
                    .ToArray());
        }
    }
}
