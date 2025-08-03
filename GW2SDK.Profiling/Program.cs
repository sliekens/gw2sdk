// This is just some code to test the performance of the GW2SDK library
// VS2022 has a built-in profiler (alt + F2), so I'm using that to see how the library performs 

using GuildWars2;
using GuildWars2.Hero.Crafting.Recipes;

using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
IHttpClientBuilder httpClientBuilder = services.AddHttpClient<Gw2Client>();
httpClientBuilder.AddStandardResilienceHandler();
var gw2Client = services.BuildServiceProvider().GetRequiredService<Gw2Client>();

await foreach (Recipe recipe in gw2Client.Hero.Crafting.Recipes.GetRecipesBulk().ValueOnly().ConfigureAwait(false))
{
    Console.WriteLine(recipe.Id);
}
