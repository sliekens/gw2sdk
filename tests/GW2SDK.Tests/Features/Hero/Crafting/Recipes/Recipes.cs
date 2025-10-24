using System.Text.Json;

using GuildWars2.Chat;

using GuildWars2.Hero.Crafting.Recipes;
using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

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

        await foreach ((Recipe actual, MessageContext context) in sut.Hero.Crafting.Recipes.GetRecipesBulk(cancellationToken: TestContext.Current!.CancellationToken))
        {

            Assert.NotNull(context);

            Assert.True(actual.Id > 0);

            Assert.True(actual.OutputItemId >= 1);

            Assert.True(actual.OutputItemCount >= 1);

            Assert.InRange(actual.MinRating, 0, 500);

            Assert.True(actual.TimeToCraft.Ticks >= 1);

            Assert.All(actual.Disciplines, discipline => Assert.True(discipline.IsDefined()));

            Assert.Empty(actual.Flags.Other);

            Assert.All(actual.Ingredients, ingredient =>
            {
                Assert.True(ingredient.Kind.IsDefined());
                Assert.True(ingredient.Id > 0);
                Assert.True(ingredient.Count > 0);
            });

            RecipeLink chatLink = actual.GetChatLink();

            Assert.Equal(actual.Id, chatLink.RecipeId);

            Assert.Equal(actual.ChatLink, chatLink.ToString());

            RecipeLink chatLinkRoundtrip = RecipeLink.Parse(chatLink.ToString());

            Assert.Equal(chatLink.ToString(), chatLinkRoundtrip.ToString());
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

        await foreach (Recipe original in sut.Hero.Crafting.Recipes.GetRecipesBulk(cancellationToken: TestContext.Current!.CancellationToken).ValueOnly(TestContext.Current!.CancellationToken))
        {

            string json = JsonSerializer.Serialize(original);

            Recipe? roundTrip = JsonSerializer.Deserialize<Recipe>(json);

            Assert.IsType(original.GetType(), roundTrip);

            Assert.Equal(original, roundTrip);
        }
    }
}
