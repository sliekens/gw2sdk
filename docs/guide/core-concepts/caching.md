# Caching Responses

GW2SDK doesn't cache responses—the right lifetime depends on the endpoint.

---

## ❄️ What to Cache

| Data | Changes | Suggested lifetime |
|------|---------|--------------------|
| **Static** (items, traits, recipes, maps) | Only when the game is updated | Hours, or until the build ID changes |
| **Account** (inventories, wallet, characters) | Whenever the player plays | Seconds to minutes |
| **Trading post** (prices, listings) | Constantly | Don't cache, or seconds |

> [!TIP]
> `MessageContext` exposes the `Date`, `Expires` and `LastModified` response headers, so the API itself can tell you how long an entry is good for.

---

## 🗃️ Caching with `IMemoryCache`

The sample below wraps `v2/traits`—one of the most static endpoints in the API—in a service which fetches every trait once and serves later calls from memory.

[!code-csharp[](~/samples/MemoryCacheUsage/Program.cs)]

Output:

```text
Cold : 999 traits in 639ms
Warm : 999 traits in 0ms
Trait 214 is 'Raging Storm'
```

### Things to Watch Out For

| Pitfall | Why it matters |
|---------|----------------|
| **Language in the cache key** | Names and descriptions are localized; without the language in the key, German callers get English text |
| **Caching per ID** | One `?ids=all` request beats 999 single-ID requests; cache the whole set and index it in memory |
| **Cache stampede** | `GetOrCreateAsync` can run the factory more than once for the same key under concurrent misses; guard it with a `SemaphoreSlim` if duplicate requests are a problem |
| **Entry size** | `MemoryCache` only evicts by size if every entry sets `ICacheEntry.Size` and the cache sets `SizeLimit` |

> [!NOTE]
> Records like `Trait` are immutable, so handing the same instance to every caller is safe.

---

## 🌐 Distributed Caching

For multiple instances, swap `IMemoryCache` for [`IDistributedCache`](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/distributed) or [`HybridCache`](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/hybrid). The pattern stays the same, but entries must be serialized—cache the JSON payload, or re-serialize the models with `System.Text.Json`.

---

## 📚 Additional Resources

| Resource | Description |
|----------|-------------|
| [Usage limits](usage-limits.md) | Rate limits, batching and throttling |
| [Cache in-memory in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/memory) | Microsoft's `IMemoryCache` guide |
| [Caching guidance](https://learn.microsoft.com/en-us/azure/architecture/best-practices/caching) | Patterns, expiration strategies and pitfalls |
