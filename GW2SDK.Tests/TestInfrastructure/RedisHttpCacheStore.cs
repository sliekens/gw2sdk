using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using GW2SDK.Http.Caching;
using StackExchange.Redis;

namespace GW2SDK.Tests.TestInfrastructure
{
    public class RedisHttpCacheStore : IHttpCacheStore
    {
        private readonly IConnectionMultiplexer redis;

        public RedisHttpCacheStore(IConnectionMultiplexer redis)
        {
            this.redis = redis ?? throw new ArgumentNullException(nameof(redis));
        }

        public async IAsyncEnumerable<ResponseCacheEntry> GetEntriesAsync(string primaryKey)
        {
            var db = redis.GetDatabase();

            var cachedResponseKeys = await db.ListRangeAsync(primaryKey);

            foreach (var cachedResponseKey in cachedResponseKeys)
            {
                var hash = await db.HashGetAllAsync((byte[])cachedResponseKey);
                if (hash.Length == 0)
                {
                    // Assume the key expired
                    await db.ListRemoveAsync(primaryKey, cachedResponseKey);
                    continue;
                }

                var retVal = new ResponseCacheEntry();
                foreach (var kvp in hash)
                {
                    if (kvp.Name == "status-code")
                    {
                        retVal.StatusCode = (int)kvp.Value;
                    }
                    else if (kvp.Name == "request-time")
                    {
                        retVal.RequestTime =
                            DateTimeOffset.ParseExact(kvp.Value.ToString(), "O", CultureInfo.InvariantCulture);
                    }
                    else if (kvp.Name == "response-time")
                    {
                        retVal.ResponseTime =
                            DateTimeOffset.ParseExact(kvp.Value.ToString(), "O", CultureInfo.InvariantCulture);
                    }
                    else if (kvp.Name == "freshness-lifetime")
                    {
                        retVal.FreshnessLifetime =
                            TimeSpan.ParseExact(kvp.Value.ToString(), "c", CultureInfo.InvariantCulture);
                    }
                    else if (kvp.Name.StartsWith("response-header-field:"))
                    {
                        var keyName = kvp.Name.ToString();
                        var fieldName = keyName.Substring(keyName.IndexOf(':') + 1);
                        retVal.ResponseHeaders[fieldName] = kvp.Value;
                    }
                    else if (kvp.Name.StartsWith("content-header-field:"))
                    {
                        var keyName = kvp.Name.ToString();
                        var fieldName = keyName.Substring(keyName.IndexOf(':') + 1);
                        retVal.ContentHeaders[fieldName] = kvp.Value;
                    }
                    else if (kvp.Name.StartsWith("vary:"))
                    {
                        var keyName = kvp.Name.ToString();
                        var fieldName = keyName.Substring(keyName.IndexOf(':') + 1);
                        retVal.SecondaryKey[fieldName] = kvp.Value;
                    }
                    else if (kvp.Name == "message-body")
                    {
                        retVal.Content = kvp.Value;
                    }
                }

                yield return retVal;
            }
        }

        public async Task StoreEntryAsync(string primaryKey, ResponseCacheEntry entry)
        {
            var db = redis.GetDatabase();
            var expires = DateTime.UtcNow.Add(entry.FreshnessLifetime - entry.CalculateAge());

            RedisKey key = primaryKey;

            // TODO: make transactional
            if (entry.Id == default)
            {
                entry.Id = Guid.NewGuid();
                await db.ListLeftPushAsync(key, entry.Id.ToByteArray());
            }

            await StoreResponse(db, entry, expires);
            await db.ListTrimAsync(key, 0, 99);
        }

        private static async Task StoreResponse(
            IDatabase db,
            ResponseCacheEntry entry,
            DateTime expires
        )
        {
            RedisKey key = entry.Id.ToByteArray();

            await db.HashSetAsync(key, "status-code", entry.StatusCode);
            await db.HashSetAsync(key, "request-time", entry.RequestTime.ToString("O", CultureInfo.InvariantCulture));
            await db.HashSetAsync(key, "response-time", entry.ResponseTime.ToString("O", CultureInfo.InvariantCulture));
            await db.HashSetAsync(key,
                "freshness-lifetime",
                entry.FreshnessLifetime.ToString("c", CultureInfo.InvariantCulture));

            foreach (var kvp in entry.ResponseHeaders)
            {
                await db.HashSetAsync(key, $"response-header-field:{kvp.Key}", kvp.Value);
            }

            foreach (var kvp in entry.ContentHeaders)
            {
                await db.HashSetAsync(key, $"content-header-field:{kvp.Key}", kvp.Value);
            }

            foreach (var kvp in entry.SecondaryKey)
            {
                await db.HashSetAsync(key, $"vary:{kvp.Key}", kvp.Value);
            }

            await db.HashSetAsync(key, "message-body", entry.Content);

            await db.KeyExpireAsync(key, expires);
        }
    }
}
