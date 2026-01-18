# Exception Handling

## 🔴 Exception Types

| Exception | When It's Thrown |
|-----------|------------------|
| `HttpRequestException` | Network failures (DNS errors, connection timeouts) |
| `BadResponseException` | API returns 4xx/5xx status or unexpected format (HTML instead of JSON) |

> [!NOTE]
> `BadResponseException` extends `HttpRequestException`, so you can catch both with a single handler—unless you need to handle specific status codes like `429`.

---

## 📝 Example

```csharp
using System;
using System.Net;
using System.Net.Http;
using GuildWars2;
using GuildWars2.Http;

using var httpClient = new HttpClient();
var gw2 = new Gw2Client(httpClient);

try
{
    var (quaggans, context) = await gw2.Quaggans.GetQuaggans();
}
catch (BadResponseException ex) when (ex.StatusCode == HttpStatusCode.TooManyRequests)
{
    Console.WriteLine("Rate limited - try again later");
}
catch (BadResponseException ex)
{
    Console.WriteLine($"API error: {ex.Message}");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Network error: {ex.Message}");
}
```
