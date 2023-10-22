## About

GW2SDK provides classes for interacting with the Guild Wars 2 API and game client. This package enables you to fetch information about the game, the player's account, PvP seasons, WvW matches and the in-game economy. It also provides realtime information from the game client such as the player's current character and the current map.

## Key features

* `Gw2Client` provides access to the Guild Wars 2 API
* `GameLink` provides realtime information from the game client (Windows only)

## How to use

How to use the `Gw2Client` to fetch the current prices of all tradable items in the game:

``` csharp
using System;
using System.Net.Http;
using GuildWars2;
using GuildWars2.Commerce.Prices;
using GuildWars2.Items;

using var httpClient = new HttpClient();
var gw2 = new Gw2Client(httpClient);

PrintHeader();
await foreach (ItemPrice itemPrice in gw2.Commerce.GetItemPricesBulk())
{
    Item item = await gw2.Items.GetItemById(itemPrice.Id);
    PrintRow(item.Name, itemPrice.BestBid, itemPrice.BestAsk);
}

void PrintHeader()
{
    Console.WriteLine(new string('=', 160));
    Console.WriteLine($"| {"Item",-50} | {"Highest buyer",-50} | {"Lowest seller",-50} |");
    Console.WriteLine(new string('=', 160));
}

void PrintRow(string item, Coin highestBuyer, Coin lowestSeller)
{
    Console.WriteLine($"| {item,-50} | {highestBuyer,-50} | {lowestSeller,-50} |");
}
```

How to use the `GameLink` to print the player's name and current map to the console:

``` csharp
using GuildWars2;
using GuildWars2.Exploration.Maps;

if (!GameLink.IsSupported())
{
    Console.WriteLine("GameLink is only supported on Windows!");
    return;
}

var refreshInterval = TimeSpan.FromSeconds(1);
using var gameLink = GameLink.Open(refreshInterval);

// The game link provides the ID of your current map,
// which you can use to look up the map name from the API
// (Pre-fetch all maps to avoid making too many requests)
var maps = await GetMaps();

gameLink.Subscribe(
    tick =>
    {
        var player = tick.GetIdentity();
        if (player != null)
        {
            var map = maps.Single(map => map.Id == player.MapId);

            Console.WriteLine($"[{tick.UiTick}] Your name is {player.Name}.");
            Console.WriteLine($"[{tick.UiTick}] You are currently in {map.Name} ({tick.Context.ServerAddress}).");
            Console.WriteLine();
        }
    });

WaitForCtrlC();

async Task<HashSet<MapSummary>> GetMaps()
{
    using var http = new HttpClient();
    var gw2 = new Gw2Client(http);
    return await gw2.Maps.GetMapSummaries();
}

void WaitForCtrlC()
{
    using var exitEvent = new ManualResetEventSlim();
    Console.CancelKeyPress += (_, _) => exitEvent.Set();
    exitEvent.Wait();
}
```

## Additional documentation

* [Documentation](https://sliekens.github.io/gw2sdk)
* [API reference](https://sliekens.github.io/gw2sdk/api)

## Feedback & contributing

GW2SDK is released as open source under the MIT license. You are welcome to create an issue if you find something is missing or broken, or a discussion for other feedback, questions or ideas.
Check out the GitHub [page](https://github.com/sliekens/gw2sdk) to find more ways to contribute.
