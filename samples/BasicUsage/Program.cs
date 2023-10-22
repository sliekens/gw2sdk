using System;
using System.Net.Http;
using GuildWars2;

// Create a Gw2Client which requires an HttpClient
using var httpClient = new HttpClient();
var gw2 = new Gw2Client(httpClient);

// Use the Gw2Client to access the API
// For example, print the game version number
var version = await gw2.Meta.GetBuild();
Console.WriteLine($"The current game version is {version.Value.Id:N0}");
Console.WriteLine();

// For example, print the current gem exchange rate for 100 gold
var gems = await gw2.Commerce.ExchangeGoldForGems(new Coin(100, 0, 0));
Console.WriteLine($"{gems.Value.GemsToReceive} gems cost 100 gold");
Console.WriteLine($"1 gem costs {gems.Value.CoinsPerGem}");
Console.WriteLine();

// For example, print raids and their wings and encounters
var raids = await gw2.Raids.GetRaids();
foreach (var raid in raids.Value)
{
    Console.WriteLine($"Raid: {raid.Id}");

    int wingNumber = 0;
    foreach (var wing in raid.Wings)
    {
        wingNumber++;
        Console.WriteLine($"  W{wingNumber}: {wing.Id}");

        int encounterNumber = 0;
        foreach (var encounter in wing.Encounters)
        {
            encounterNumber++;
            Console.WriteLine($"    Encounter {encounterNumber}: {encounter.Id} ({encounter.Kind})");
        }
    }

    Console.WriteLine();
}
