using GuildWars2.Hero.Crafting.Recipes;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

[ServiceDataSource]
public class RecipesByIngredient(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        // Normally the limit for ids=all is 200 items
        //   but that doesn't seem to apply for recipes search by input/output item
        // There are 800+ recipes that require a vision crystal
        const int visionCrystal = 46746;
        (IImmutableValueSet<Recipe> actual, MessageContext context) = await sut.Hero.Crafting.Recipes.GetRecipesByIngredientItemId(visionCrystal, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual.Count).IsGreaterThan(200);
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        foreach (Recipe recipe in actual)
        {
            await Assert.That(recipe.Ingredients).Contains(ingredient => ingredient.Id == visionCrystal);
        }
    }
}
