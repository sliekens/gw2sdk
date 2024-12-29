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
