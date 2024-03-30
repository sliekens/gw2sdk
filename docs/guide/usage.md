# Basic usage

The entry point for API access is `GuildWars2.Gw2Client`. From there you can use IntelliSense to discover resources. The class consists of logical groups containing related sets of APIs. For example, all APIs pertaining to the trading post are grouped into `Gw2Client.Commerce` and all APIs pertaining to items are grouped into `Gw2Client.Items`.

The `Gw2Client` has a single dependency on `System.Net.Http.HttpClient` which you must provide from your application code.

The query methods on the `Gw2Client` return `Task` objects that you can use to await the result. The result is a tuple which contains 2 items:

1. A strongly typed object that represents the body of the API response
2. A `MessageContext` object which contains HTTP response message headers

``` csharp
using System;
using System.Net.Http;
using GuildWars2;

using var httpClient = new HttpClient();

var gw2 = new Gw2Client(httpClient);

// Awaiting GetQuaggans results in a tuple of Quaggans and a MessageContext
foreach (var (quaggans, context) in await gw2.Quaggans.GetQuaggans())
{
    Console.WriteLine(quaggan.Id);
    Console.WriteLine(quaggan.PictureHref);
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
    .AsDictionary(map => map.Id)
    .ValueOnly();

// Now you can easily access maps by their ID
MapSummary queensdale = maps[15];
```

## Example: print a table of best buy and sell offers of all tradable items

This sample illustrates the following concepts:

1. How to use the `Gw2Client` to access the item prices API
2. How to use `await foreach` to iterate over a stream of values
3. How to use the `Id` property to access the item details API
4. What happens if you make too many requests

[!code-csharp[](../../samples/BasicUsage/Program.cs)]

Output

``` text
================================================================================================================================================================
| Item                                               | Highest buyer                                      | Lowest seller                                      |
================================================================================================================================================================
| Sealed Package of Snowballs                        | 1 silver, 52 copper                                | 2 silver, 84 copper                                |
| Mighty Country Coat                                | 84 copper                                          | 1 silver, 16 copper                                |
| Mighty Country Coat                                | 31 copper                                          | 87 copper                                          |
| Mighty Studded Coat                                | 29 copper                                          | 71 copper                                          |
| Mighty Worn Chain Greaves                          | 35 copper                                          | 75 copper                                          |
| Berserker's Sneakthief Mask of the Afflicted       | 3 gold, 31 silver, 49 copper                       | 10 gold, 38 silver, 74 copper                      |
| Berserker's Sneakthief Mask of Dwayna              | 13 silver, 50 copper                               | 37 silver, 26 copper                               |
| Mighty Worn Chain Greaves                          | 2 silver, 6 copper                                 | 4 silver, 50 copper                                |
| Berserker's Sneakthief Mask of Strength            | 22 silver, 93 copper                               | 30 silver, 33 copper                               |
| Berserker's Seer Coat of the Flame Legion          | 4 gold, 41 silver, 66 copper                       | 24 gold, 41 silver, 65 copper                      |
| Mighty Studded Coat                                | 19 copper                                          | 84 copper                                          |
| Strong Worn Scale Boots                            | 80 copper                                          | 1 silver, 70 copper                                |
| Berserker's Seer Coat of the Traveler              | 21 silver, 89 copper                               | 43 silver, 39 copper                               |

...

| Box of Simple Mighty Chain Armor                   | 1 silver, 2 copper                                 | 76 silver, 71 copper                               |
| Box of Malign Chain Armor                          | 11 silver, 17 copper                               | 1 gold, 79 silver, 97 copper                       |
| Box of Resilient Chain Armor                       | 35 copper                                          | 1 gold, 59 silver                                  |
| Box of Mighty Chain Armor                          | 20 silver, 9 copper                                | 99 silver, 95 copper                               |
| Box of Vital Chain Armor                           | 16 silver                                          | 1 gold, 99 silver, 97 copper                       |
| Box of Healing Chain Armor                         | 10 silver, 59 copper                               | 1 gold, 98 silver, 92 copper                       |
| Box of Precise Chain Armor                         | 5 silver, 21 copper                                | 1 gold, 21 silver, 11 copper                       |
| Box of Malign Chain Armor                          | 61 copper                                          | 3 gold, 29 silver, 95 copper                       |
Unhandled exception. GuildWars2.Http.TooManyRequestsException: too many requests
   at GuildWars2.Http.HttpResponseMessageExtensions.EnsureResult(HttpResponseMessage instance, CancellationToken cancellationToken) in X:\src\GW2SDK\GW2SDK\Internal\Http\HttpResponseMessageExtensions.cs:line 164
   at GuildWars2.Items.Http.ItemByIdRequest.SendAsync(HttpClient httpClient, CancellationToken cancellationToken) in X:\src\GW2SDK\GW2SDK\Features\Items\Http\ItemByIdRequest.cs:line 43
   at Program.<Main>$(String[] args) in X:\src\GW2SDK\samples\BasicUsage\Program.cs:line 18
   at Program.<Main>$(String[] args) in X:\src\GW2SDK\samples\BasicUsage\Program.cs:line 14
   at Program.<Main>(String[] args)
```

### Too many requests?

This code is not optimized in the sense that it makes a request for each item individually inside the `foreach` loop. The API has a rate limit, so this code will fail with a `TooManyRequestsException` after a short while.

More information about usage limits can be found in the error handling section of this site, with tips on how to solve them.
