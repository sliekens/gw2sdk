# Exception handling

If a total request failure occurs (such as DNS resolution errors), the `Gw2Client`
throws `HttpRequestException`, which is the same behavior as if you used `HttpClient`
directly.

If the request is successful, but the response cannot be used, the `Gw2Client`
throws `BadResponseException`. This can happen when the API returns a 4xx or 5xx
status code, or when the response is not in the expected format (such as HTML instead
of JSON). `BadResponseException` is a subclass of `HttpRequestException`, so you
don't need to catch it separately, unless you need to handle specific HTTP statuses
like `429 Too Many Requests`.

The following example demonstrates which exceptions you might need to catch:

``` csharp
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
catch (BadResponseException badResponse) when (badResponse.StatusCode == HttpStatusCode.TooManyRequests)
{
    Console.WriteLine("Too many requests");
}
catch (BadResponseException badResponse)
{
    Console.WriteLine("API returned an unusable response: " + badResponse.Message);
}
catch (HttpRequestException httpRequestException)
{
    Console.WriteLine("General request failure: " + httpRequestException.Message);
}
```
