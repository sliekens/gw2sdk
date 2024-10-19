## About

GW2SDK provides classes for interacting with the Guild Wars 2 API and game client.
This package enables you to fetch information about the game, the player's account,
PvP seasons, WvW matches and the in-game economy. It also provides realtime information
from the game client such as the player's current character and the current map.

## Key features

* `Gw2Client` provides access to the Guild Wars 2 API
* `GameLink` provides realtime information from the game client (Windows only)

## How to use

API access using `Gw2Client`:

``` csharp
using System;
using System.Net.Http;
using System.Threading.Tasks;
using GuildWars2;
using GuildWars2.Commerce.Prices;
using GuildWars2.Items;

namespace PackageReadme.Gw2ClientProgram;

internal class Program
{
    public static async Task Main(string[] args)
    {
        // Create an instance of the Gw2Client, which depends on HttpClient
        using var httpClient = new HttpClient();
        var gw2 = new Gw2Client(httpClient);

        // Fetch the current prices of all items
        await foreach (var itemPrice in gw2.Commerce.GetItemPricesBulk().ValueOnly())
        {
            // The item price contains the item's ID, which can be used to fetch
            // the item's name
            var item = await gw2.Items.GetItemById(itemPrice.Id).ValueOnly();

            // Print the item's name and its current highest buyer and lowest seller
            PrintItem(item, itemPrice);
        }
    }

    private static void PrintItem(Item item, ItemPrice price)
    {
        Console.WriteLine($"{"Item",-15}: {item.Name}");
        Console.WriteLine($"{"Highest buyer",-15}: {price.BestBid}");
        Console.WriteLine($"{"Lowest seller",-15}: {price.BestAsk}");
        Console.WriteLine($"{"Bid-ask spread",-15}: {price.BidAskSpread}");
        Console.WriteLine();
    }
}

```

Game client access using `GameLink`:

(This example also requires the System.Reactive package to be installed.)

``` csharp
using System;
using System.Net.Http;
using System.Threading.Tasks;
using GuildWars2;

namespace PackageReadme.GameLinkProgram;

internal class Program
{
    public static async Task Main(string[] args)
    {
        if (!GameLink.IsSupported())
        {
            Console.WriteLine("GameLink is only supported on Windows!");
            return;
        }

        Console.WriteLine("GameLink is starting! (Ensure the game is running"
            + " and that you are loaded into a map.)");

        // Pre-fetch all maps from the API, they are used to display the player's
        // current map
        using var http = new HttpClient();
        var gw2 = new Gw2Client(http);
        var maps = await gw2.Exploration.GetMapSummaries()
            .AsDictionary(map => map.Id)
            .ValueOnly();

        // Choose an interval to indicate how often you want to receive fresh data
        // from the game
        // For example, at most once every second
        // Default: no limit, every change in the game state will be available immediately
        var refreshInterval = TimeSpan.FromSeconds(1);
        
        // Open the game link with the chosen refresh interval
        var gameLink = GameLink.Open(refreshInterval);
        
        // Subscribe to the game link to start receiving game state updates
        var subscription = gameLink.Subscribe(
            tick =>
            {
                // Each 'tick' contains information about the player's character
                // and actions, among other things
                var player = tick.GetIdentity();

                // The identity can be missing due to JSON errors, always check
                // for null
                if (player != null)
                {
                    // Use the player's map ID to find the map name in the
                    // pre-fetched list of maps
                    var map = maps[player.MapId];

                    // Print the player's name and current map
                    Console.WriteLine($"[{tick.UiTick}] Your name is {player.Name}.");
                    Console.WriteLine(
                        $"[{tick.UiTick}] You are currently in {map.Name} ({tick.Context.ServerAddress})."
                    );
                    Console.WriteLine();
                }
            }
        );

        // Wait for the user to press Enter
        Console.ReadLine();

        // Stop the data stream and close the game link
        subscription.Dispose();
        gameLink.Dispose();
    }
}

```

## Additional documentation

* [Documentation](https://sliekens.github.io/gw2sdk)
* [API reference](https://sliekens.github.io/gw2sdk/api)

## Feedback & contributing

GW2SDK is released as open source under the MIT license. You are welcome to create
an issue if you find something is missing or broken, or a discussion for other
feedback, questions or ideas. Check out the GitHub [page](https://github.com/sliekens/gw2sdk)
to find more ways to contribute.
