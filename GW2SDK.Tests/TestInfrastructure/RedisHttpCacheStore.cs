using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
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

            var cachedResponseKeys = await db.ListRangeAsync(primaryKey)
                .ConfigureAwait(false);

            foreach (var cachedResponseKey in cachedResponseKeys)
            {
                var hash = await db.HashGetAllAsync((byte[])cachedResponseKey)
                    .ConfigureAwait(false);
                if (hash.Length == 0)
                {
                    // Assume the key expired
                    await db.ListRemoveAsync(primaryKey, cachedResponseKey)
                        .ConfigureAwait(false);
                    continue;
                }

                var retVal = new ResponseCacheEntry
                {
                    Key = primaryKey,
                    Id = new Guid((byte[])cachedResponseKey)
                };

                using var response = new HttpResponseMessage();
                foreach (var kvp in hash)
                {
                    if (kvp.Name == "status-code")
                    {
                        retVal.StatusCode = (int)kvp.Value;
                    }
                    else if (kvp.Name == "request-time")
                    {
                        retVal.RequestTime = DateTimeOffset.ParseExact(
                            kvp.Value.ToString(),
                            "O",
                            CultureInfo.InvariantCulture
                        );
                    }
                    else if (kvp.Name == "response-time")
                    {
                        retVal.ResponseTime = DateTimeOffset.ParseExact(
                            kvp.Value.ToString(),
                            "O",
                            CultureInfo.InvariantCulture
                        );
                    }
                    else if (kvp.Name == "freshness-lifetime")
                    {
                        retVal.TimeToLive = TimeSpan.ParseExact(
                            kvp.Value.ToString(),
                            "c",
                            CultureInfo.InvariantCulture
                        );
                    }
                    else if (kvp.Name.StartsWith("response-header-field:"))
                    {
                        var keyName = kvp.Name.ToString();
                        var fieldName = keyName.Substring(keyName.IndexOf(':') + 1);
                        retVal.ResponseHeaders.TryAddWithoutValidation(fieldName, JsonSerializer.Deserialize<string[]>((string)kvp.Value));
                    }
                    else if (kvp.Name.StartsWith("content-header-field:"))
                    {
                        var keyName = kvp.Name.ToString();
                        var fieldName = keyName.Substring(keyName.IndexOf(':') + 1);
                        retVal.ContentHeaders.TryAddWithoutValidation(fieldName, JsonSerializer.Deserialize<string[]>((string)kvp.Value));
                    }
                    else if (kvp.Name.StartsWith("vary:"))
                    {
                        var keyName = kvp.Name.ToString();
                        var fieldName = keyName.Substring(keyName.IndexOf(':') + 1);
                        retVal.SecondaryKey[fieldName] = kvp.Value;
                    }
                    else if (kvp.Name == "claims-principal")
                    {
                        retVal.ClaimsPrincipalDigest = kvp.Value;
                    }
                    else if (kvp.Name == "message-body")
                    {
                        retVal.Content = kvp.Value;
                    }
                }

                yield return retVal;
            }
        }

        public async Task StoreEntryAsync(
            ResponseCacheEntry entry,
            CancellationToken cancellationToken
        )
        {
            var db = redis.GetDatabase();

            var ttl = entry.TimeToLive - entry.CurrentAge();

            // Add a bit of margin for processing responses with a small freshness
            ttl += TimeSpan.FromMinutes(5);

            // TODO: make transactional
            if (entry.Id == default)
            {
                entry.Id = Guid.NewGuid();
                var length = await db.ListLeftPushAsync(entry.Key, entry.Id.ToByteArray())
                    .ConfigureAwait(false);
                if (length > 100)
                {
                    await db.ListTrimAsync(
                            entry.Key,
                            0,
                            99
                        )
                        .ConfigureAwait(false);
                }
            }

            await StoreResponse(
                    db,
                    entry,
                    ttl,
                    cancellationToken
                )
                .ConfigureAwait(false);
        }

        private static async Task StoreResponse(
            IDatabase db,
            ResponseCacheEntry entry,
            TimeSpan ttl,
            CancellationToken cancellationToken
        )
        {
            RedisKey key = entry.Id.ToByteArray();
            var entries = new List<HashEntry>
            {
                new("status-code", entry.StatusCode),
                new("request-time", entry.RequestTime.ToString("O", CultureInfo.InvariantCulture)),
                new("response-time", entry.ResponseTime.ToString("O", CultureInfo.InvariantCulture)),
                new("freshness-lifetime", entry.TimeToLive.ToString("c", CultureInfo.InvariantCulture)),
                new("message-body", entry.Content)
            };

            foreach (var field in entry.ResponseHeaders)
            {
                entries.Add(new HashEntry($"response-header-field:{field.Key}", JsonSerializer.Serialize(field.Value)));
            }

            foreach (var field in entry.ContentHeaders)
            {
                entries.Add(new HashEntry($"content-header-field:{field.Key}", JsonSerializer.Serialize(field.Value)));
            }

            foreach (var field in entry.SecondaryKey)
            {
                entries.Add(new HashEntry($"vary:{field.Key}", field.Value));
            }

            if (entry.ClaimsPrincipalDigest.Length != 0)
            {
                entries.Add(new HashEntry("claims-principal", entry.ClaimsPrincipalDigest));
            }

            await db.HashSetAsync(key, entries.ToArray())
                .ConfigureAwait(false);
            await db.KeyExpireAsync(key, ttl)
                .ConfigureAwait(false);
        }
    }
}
