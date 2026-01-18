# Usage Limits

The Guild Wars 2 API has rate limits to ensure fair access for all users.

---

## 📊 Rate Limit Details

| | |
|---|---|
| **Official limit** | 600 requests/minute |
| **Actual limit** | ~300 requests/minute (community-discovered) |
| **Scope** | Per IP address (shared across all apps/devices) |
| **Error response** | HTTP `429 Too Many Requests` |

---

## 🛡️ Avoiding Rate Limits

### 1️⃣ Caching

Cache static or rarely-changing data (items, recipes, maps) and refresh only when stale.

| Tool | Use Case |
|------|----------|
| `MemoryCache` | Single-instance apps |
| Redis | Distributed systems |

> [!TIP]
> Check `Last-Modified` and `Expires` headers to determine freshness.

### 2️⃣ Batching

Combine multiple requests into one using comma-separated IDs:

```csharp
// ❌ 10 requests
for (int i = 1; i <= 10; i++)
    await gw2.Items.GetItemById(i);

// ✅ 1 request
await gw2.Items.GetItemsByIds([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);
```

> [!WARNING]
> **Hard limit:** 200 IDs per request. Exceeding this throws `ArgumentException`.

> [!TIP]
> Need more than 200? Use bulk methods like `GetItemsBulk()` which automatically fan out into parallel requests and return `IAsyncEnumerable<T>`.

### 3️⃣ Throttling

Limit your request rate using timers, queues, or rate-limiting libraries.

---

## 🚨 Handling Rate Limit Errors

When rate limited, you receive `BadResponseException` with `StatusCode = 429`.

| App Type | Recommendation |
|----------|----------------|
| **Interactive** (UI, bots, CLI) | Show a friendly message, use cached/fallback data |
| **Background** (jobs, queues) | Retry with exponential backoff, log for analysis |

> [!NOTE]
> GW2SDK doesn't auto-retry rate limits—handling depends on your use case. See [Resiliency](resiliency.md) for Polly integration.
