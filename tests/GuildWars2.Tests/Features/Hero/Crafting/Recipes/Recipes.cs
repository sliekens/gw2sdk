using System.Text.Json;

using GuildWars2.Chat;
using GuildWars2.Hero.Crafting;
using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Hero.Crafting.Recipes;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

[NotInParallel("Recipes")]
public class Recipes
{
    [Test]
    public async Task Can_be_enumerated()
    {
        // The JsonLinesHttpMessageHandler simulates the behavior of the real API
        // because bulk enumeration quickly exhausts the API rate limit
        using JsonLinesHttpMessageHandler handler = new("Data/recipes.jsonl.gz");
        using HttpClient httpClient = new(handler);
        Gw2Client sut = new(httpClient);
        await foreach ((Recipe actual, MessageContext context) in sut.Hero.Crafting.Recipes.GetRecipesBulk(cancellationToken: TestContext.Current!.Execution.CancellationToken))
        {
            await Assert.That(context).IsNotNull();
            await Assert.That(actual.Id).IsGreaterThan(0);
            await Assert.That(actual.OutputItemId).IsGreaterThanOrEqualTo(1);
            await Assert.That(actual.OutputItemCount).IsGreaterThanOrEqualTo(1);
            await Assert.That(actual.MinRating).IsGreaterThanOrEqualTo(0).And.IsLessThanOrEqualTo(500);
            await Assert.That(actual.TimeToCraft.Ticks).IsGreaterThanOrEqualTo(1);
            foreach (Extensible<CraftingDisciplineName> discipline in actual.Disciplines)
            {
                await Assert.That(discipline.IsDefined()).IsTrue();
            }

            await Assert.That(actual.Flags.Other).IsEmpty();
            foreach (Ingredient ingredient in actual.Ingredients)
            {
                await Assert.That(ingredient.Kind.IsDefined()).IsTrue();
                await Assert.That(ingredient.Id).IsGreaterThan(0);
                await Assert.That(ingredient.Count).IsGreaterThan(0);
            }

            RecipeLink chatLink = actual.GetChatLink();
            await Assert.That(chatLink.RecipeId).IsEqualTo(actual.Id);
            await Assert.That(actual.ChatLink).IsEqualTo(chatLink.ToString());
            RecipeLink chatLinkRoundtrip = RecipeLink.Parse(chatLink.ToString());
            await Assert.That(chatLinkRoundtrip.ToString()).IsEqualTo(chatLink.ToString());
        }
    }

    [Test]
    public async Task Can_be_serialized()
    {
        // The JsonLinesHttpMessageHandler simulates the behavior of the real API
        // because bulk enumeration quickly exhausts the API rate limit
        using JsonLinesHttpMessageHandler handler = new("Data/recipes.jsonl.gz");
        using HttpClient httpClient = new(handler);
        Gw2Client sut = new(httpClient);
        await foreach (Recipe original in sut.Hero.Crafting.Recipes.GetRecipesBulk(cancellationToken: TestContext.Current!.Execution.CancellationToken).ValueOnly(TestContext.Current!.Execution.CancellationToken))
        {
#if NET
            string json = JsonSerializer.Serialize(original, Common.TestJsonContext.Default.Recipe);
            Recipe? roundTrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.Recipe);
#else
            string json = JsonSerializer.Serialize(original);
            Recipe? roundTrip = JsonSerializer.Deserialize<Recipe>(json);
#endif
            await Assert.That(roundTrip).IsTypeOf<Recipe>();
            await Assert.That(roundTrip).IsEqualTo(original);
        }
    }
}
