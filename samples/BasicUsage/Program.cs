using System;
using System.Net.Http;
using GuildWars2;

// Create a Gw2Client which requires an HttpClient
using var httpClient = new HttpClient();
var gw2 = new Gw2Client(httpClient);

PrintHeader();

// Get the trading post prices for all items in bulk
await foreach (var itemPrice in gw2.Commerce.GetItemPricesBulk().ValueOnly())
{
    // ItemPrice contains an item ID, BestBid, and BestAsk
    // Use the ID to get the item details
    var item = await gw2.Items.GetItemById(itemPrice.Id).ValueOnly();

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
