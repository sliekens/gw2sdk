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
using System.Threading.Tasks;
using GuildWars2;

namespace PackageReadme;

internal class Gw2ClientProgram
{
    public static async Task Main(string[] args)
    {
        // Create an instance of the Gw2Client, which depends on HttpClient
        using var httpClient = new HttpClient();
        var gw2 = new Gw2Client(httpClient);

        // Print a table header
        PrintTableHeader();

        // Fetch the current prices of all items
        await foreach (var (itemPrice, _) in gw2.Commerce.GetItemPricesBulk())
        {
            // The item price contains the item's ID, which can be used to fetch the item's name
            var (item, _) = await gw2.Items.GetItemById(itemPrice.Id);

            // Print the item's name and its current highest buyer and lowest seller
            PrintTableRow(item.Name, itemPrice.BestBid, itemPrice.BestAsk);
        }

        void PrintTableHeader()
        {
            /*
            ================================================================================================================================================================
            | Item                                               | Highest buyer                                      | Lowest seller                                      |
            ================================================================================================================================================================
             */
            Console.WriteLine(new string('=', 160));
            Console.WriteLine($"| {"Item",-50} | {"Highest buyer",-50} | {"Low

        void PrintTableHeader()est seller",-50} |");
            Console.WriteLine(new string('=', 160));
        }

        void PrintTableRow(string item, Coin highestBuyer, Coin lowestSeller)
        {
            /*
             | <item>                                             | <highestBuyer>                                     | <lowestSeller>                                     |
             */
            Console.WriteLine($"| {item,-50} | {highestBuyer,-50} | {lowestSeller,-50} |");
        }
    }
}

```

How to use the `GameLink` to print the player's name and current map to the console:

``` csharp
using GuildWars2;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GuildWars2;

namespace PackageReadme;

internal class GameLinkProgram
{
    public static async Task Main(string[] args)
    {
        if (!GameLink.IsSupported())
        {
            Console.WriteLine("GameLink is only supported on Windows!");
            return;
        }

        Console.WriteLine("GameLink is starting! (Ensure the game is running and that you are loaded into a map.)");


        // Pre-fetch all maps from the API, they are used to display the player's current map
        using var http = new HttpClient();
        var gw2 = new Gw2Client(http);
        var (maps, _) = await gw2.Exploration.GetMapSummaries();

        // Choose an interval to indicate how often you want to receive fresh data from the game
        // For example, at most once every second
        // Default: no limit, every change in the game state will be available immediately
        var refreshInterval = TimeSpan.FromSeconds(1);
        
        // Open the game link with the chosen refresh interval
        var gameLink = GameLink.Open(refreshInterval);
        
        // Subscribe to the game link to start receiving game state updates
        var subscription = gameLink.Subscribe(
            tick =>
            {
                // Each 'tick' contains information about the player's character and actions, among other things
                var player = tick.GetIdentity();

                // The identity can be missing due to JSON errors, always check for null
                if (player != null)
                {
                    // Use the player's map ID to find the map name in the pre-fetched list of maps
                    var map = maps.Single(map => map.Id == player.MapId);

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

GW2SDK is released as open source under the MIT license. You are welcome to create an issue if you find something is missing or broken, or a discussion for other feedback, questions or ideas.
Check out the GitHub [page](https://github.com/sliekens/gw2sdk) to find more ways to contribute.
