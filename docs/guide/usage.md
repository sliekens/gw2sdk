# Basic usage

The entry point for API access is `GuildWars2.Gw2Client`. From there you can use IntelliSense to discover resources. The class consists of logical groups containing related sets of APIs. For example, all APIs pertaining to the trading post are grouped into `Gw2Client.Commerce` and all APIs pertaining to items are grouped into `Gw2Client.Items`.

> [!Video https://www.youtube.com/embed/_beHV1rfq0M]

The `Gw2Client` has a single dependency on `System.Net.Http.HttpClient` which you must provide from your application code.

The query methods on the `Gw2Client` return `Task` objects that you can use to await the result. The result is a tuple which contains 2 items:

1. The requested value(s)
2. A `MessageContext` object which contains HTTP response message headers

If a request error occurs (such as DNS resolution errors), the `Gw2Client` throws `HttpRequestException`, which is the same behavior as if you used `HttpClient` directly.

If the request is successful, but the response cannot be used, the `Gw2Client` throws `BadResponseException`. This can happen when the API returns a 4xx or 5xx status code, or when the response is not in the expected format (such as HTML instead of JSON). `BadResponseException` is a subclass of `HttpRequestException`, so you don't need to catch it separately.

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
    // Awaiting GetQuaggans results in a tuple of Quaggans and a MessageContext
    var (quaggans, context) = await gw2.Quaggans.GetQuaggans();
    foreach (var quaggan in quaggans)
    {
        Console.WriteLine(quaggan.Id);
        Console.WriteLine(quaggan.ImageHref);
    }

    Console.WriteLine("Found " + context.ResultCount + " Quaggans");
    Console.WriteLine("Generated on " + context.Date);
}
catch (BadResponseException badResponse) when (badResponse.StatusCode == HttpStatusCode.TooManyRequests)
{
    Console.WriteLine("Too many requests");
}
catch (HttpRequestException httpRequestException)
{
    Console.WriteLine("General request error: " + httpRequestException.Message);
}
```

The first item of the tuple contains the requested data. The second `MessageContext` object contains metadata from HTTP response headers. Advanced users might use it to access caching-related response headers. For simple usage, use the discard operator `_`.

``` csharp
// Discard the MessageContext if you don't need it
var (quaggans, _) = await gw2.Quaggans.GetQuaggans();
```

You can also use the `ValueOnly()` extension method to discard the `MessageContext` object. This is just a convenience method to make the code more readable.

``` csharp
var quaggans = await gw2.Quaggans.GetQuaggans().ValueOnly();
```

### Cross-referencing data from multiple sources

The API returns highly normalized data, which means that you often need to make multiple requests to get all the data you need. For example, to print a table of trading post items with their best buy and sell offers, you need to make a request to the item prices API and then cross-reference it with the item details API to get the item names. This is a common pattern in the Guild Wars 2 API.

There is a helper method `AsDictionary()` that you can use to make cross-referencing easier in some cases.

``` csharp
// Create a dictionary of maps keyed by their ID (and discard the MessageContext)
var maps = await gw2.Exploration.GetMapSummaries()
    .AsDictionary(static map => map.Id)
    .ValueOnly();

// Now you can easily access maps by their ID
MapSummary queensdale = maps[15];
```

## Example: print a table of best buy and sell offers of all tradable items

This sample illustrates the following concepts:

1. How to use the `Gw2Client` to access the item prices API with `GetItemPricesBulk`
2. How to use the `Id` property to access the item details API with `GetItemById`

For simplicity, `GetItemById` is called inside the loop, which is not recommended. Instead, you should use batching to get multiple items in a single request. For example, you could use `GetItemsBulk` to create a local copy of all items, and then use that copy to cross-reference item prices.


[!code-csharp[](../../samples/BasicUsage/Program.cs)]

Output

``` text
================================================================================================================================================================
| Item                                               | Highest buyer                                      | Lowest seller                                      |
================================================================================================================================================================
| Strong Rawhide Bracers                             | 52 copper                                          | 3 silver, 50 copper                                |
| Strong Rawhide Mask                                | 1 silver                                           | 5 silver                                           |
| Strong Rawhide Leggings                            | 1 silver, 26 copper                                | 4 silver, 50 copper                                |
| Strong Rawhide Shoulders                           | 35 copper                                          | 7 silver, 32 copper                                |
| Berserker's Chainmail Footgear                     | 33 copper                                          | 3 silver, 70 copper                                |
| Berserker's Chainmail Chestpiece                   | 55 copper                                          | 7 silver, 55 copper                                |
| Berserker's Chainmail Gauntlets                    | 30 copper                                          | 6 silver, 96 copper                                |
| Berserker's Chainmail Helm                         | 43 copper                                          | 10 silver, 37 copper                               |
| Berserker's Chainmail Leggings                     | 78 copper                                          | 23 silver, 98 copper                               |
| Berserker's Chainmail Shoulders                    | 50 copper                                          | 19 silver, 94 copper                               |
| Berserker's Apprentice Shoes                       | 31 copper                                          | 2 silver                                           |
| Berserker's Apprentice Coat                        | 90 copper                                          | 26 silver, 76 copper                               |
| Berserker's Apprentice Gloves                      | 30 copper                                          | 10 silver, 27 copper                               |
| Berserker's Apprentice Band                        | 43 copper                                          | 5 silver, 39 copper                                |
| Berserker's Apprentice Pants                       | 57 copper                                          | 19 silver, 97 copper                               |
```
