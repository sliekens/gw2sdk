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
        var subscription = gameLink.Subscribe(gameTick =>
            {
                // Each 'tick' contains information about the player's character
                // and actions, among other things.
                var player = gameTick.GetIdentity();

                // The identity can be missing due to JSON errors,
                // always check for null.
                if (player is not null)
                {
                    Console.WriteLine($"{player.Name} is ready to go!");
                    Console.WriteLine(
                        $"Race              : {player.Race}"
                    );
                    Console.WriteLine(
                        $"Profession        : {player.Profession}"
                    );
                    Console.WriteLine(
                        $"Specialization ID : {player.SpecializationId}"
                    );
                    Console.WriteLine(
                        $"Squad leader      : {player.Commander}"
                    );
                    Console.WriteLine(
                        $"In combat         : {gameTick.Context.UiState.HasFlag(UiState.IsInCombat)}"
                    );
                    Console.WriteLine(
                        $"Current mount     : {gameTick.Context.Mount}"
                    );
                    Console.WriteLine(
                        $"Tick              : {gameTick.UiTick}"
                    );
                }

                Console.WriteLine();
            }
        );

        // Wait for the user to press Enter.
        Console.ReadLine();

        subscription.Dispose();
    }
}
