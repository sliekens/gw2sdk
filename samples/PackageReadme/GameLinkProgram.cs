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
        var maps = await gw2.Exploration.GetMapSummaries()
            .AsDictionary(map => map.Id)
            .ValueOnly();

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
