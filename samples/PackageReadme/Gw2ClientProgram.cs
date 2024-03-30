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
        await foreach (var itemPrice in gw2.Commerce.GetItemPricesBulk().ValueOnly())
        {
            // The item price contains the item's ID, which can be used to fetch the item's name
            var item = await gw2.Items.GetItemById(itemPrice.Id).ValueOnly();

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
            Console.WriteLine($"| {"Item",-50} | {"Highest buyer",-50} | {"Lowest seller",-50} |");
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
