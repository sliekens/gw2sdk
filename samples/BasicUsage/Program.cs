using GuildWars2;
using GuildWars2.Commerce.Prices;
using GuildWars2.Items;

// Create a Gw2Client which requires an HttpClient
using HttpClient httpClient = new();
Gw2Client gw2 = new(httpClient);

// Get the trading post prices for all items in bulk
await foreach (ItemPrice itemPrice in gw2.Commerce.GetItemPricesBulk().ValueOnly())
{
    // ItemPrice contains an item ID, BestBid, and BestAsk
    // Use the item ID to get the item details
    Item item = await gw2.Items.GetItemById(itemPrice.Id).ValueOnly();

    PrintItem(item, itemPrice);
}

static void PrintItem(Item item, ItemPrice price)
{
    Console.WriteLine($"{"Item",-15}: {item.Name}");
    Console.WriteLine($"{"Highest buyer",-15}: {price.BestBid}");
    Console.WriteLine($"{"Lowest seller",-15}: {price.BestAsk}");
    Console.WriteLine($"{"Bid-ask spread",-15}: {price.BidAskSpread}");
    Console.WriteLine();
}
