// This is just some code to test the performance of the GW2SDK library
// VS2022 has a built-in profiler (alt + F2), so I'm using that to see how the library performs 

using GuildWars2;

using var httpClient = new HttpClient();
var gw2Client = new Gw2Client(httpClient);

await foreach (var (recipe, _) in gw2Client.Hero.Crafting.Recipes.GetRecipesBulk())
{
    Console.WriteLine(recipe.Id);
}
