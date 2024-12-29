## About

GW2SDK provides classes for interacting with the Guild Wars 2 API and game client.
This package enables you to fetch information about the game, the player's account,
PvP seasons, WvW matches and the in-game economy. It also provides realtime information
from the game client such as the player's current character and the current map.

## Key features

* `Gw2Client` provides access to the Guild Wars 2 API
* `GameLink` provides realtime information from the game client (Windows only)

## How to use Gw2Client

API access using `Gw2Client`:

``` csharp
using System;
using System.Net.Http;
using System.Threading.Tasks;
using GuildWars2;
using GuildWars2.Commerce.Prices;
using GuildWars2.Items;

namespace Gw2ClientProgram;

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

## How to use GameLink

Game client access using `GameLink`:

(This example also requires the System.Reactive package to be installed.)

``` csharp
using System;
using System.Threading.Tasks;
using GuildWars2;
using GuildWars2.Mumble;

namespace GameLinkProgram;

internal class Program
{
    public static async Task Main(string[] args)
    {
        if (!GameLink.IsSupported())
        {
            Console.WriteLine("GameLink is only supported on Windows!");
            return;
        }

        // Choose an interval to indicate how often you want to
        //   receive fresh data from the game.
        // For example, at most once every second.
        // Default: no limit, every change in the game state
        //   will be available immediately.
        var refreshInterval = TimeSpan.FromSeconds(1);

        // Open the game link with the chosen refresh interval.
        // GameLink implements IDiposable and IAsyncDisposable,
        //  make sure it is disposed one way or another,
        //  e.g. by 'using' or 'await using'.
        await using var gameLink = GameLink.Open(refreshInterval);

        Console.WriteLine(
            "GameLink is starting! (Ensure the game is running"
            + " and that you are loaded into a map.)"
        );

        // Subscribe to the game link to start receiving game state updates.
        var subscription = gameLink.Subscribe(
            gameTick =>
            {
                // Each 'tick' contains information about the player's character
                // and actions, among other things.
                var player = gameTick.GetIdentity();

                // The identity can be missing due to JSON errors,
                // always check for null.
                if (player is not null)
                {
                    Console.WriteLine($"{player.Name} is ready to go!");
                    Console.WriteLine($"Race              : {
                        player.Race
                    }");
                    Console.WriteLine($"Profession        : {
                        player.Profession
                    }");
                    Console.WriteLine($"Specialization ID : {
                        player.SpecializationId
                    }");
                    Console.WriteLine($"Squad leader      : {
                        player.Commander
                    }");
                    Console.WriteLine($"In combat         : {
                        gameTick.Context.UiState.HasFlag(UiState.IsInCombat)
                    }");
                    Console.WriteLine($"Current mount     : {
                        gameTick.Context.Mount
                    }");
                    Console.WriteLine($"Tick              : {
                        gameTick.UiTick
                    }");
                }

                Console.WriteLine();
            }
        );

        // Wait for the user to press Enter.
        Console.ReadLine();

        subscription.Dispose();
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
