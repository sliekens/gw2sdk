using GuildWars2.Hero.Crafting.Recipes;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

[ServiceDataSource]
public class RecipesByIngredientByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int visionCrystal = 46746;
        const int pageSize = 3;
        (IImmutableValueSet<Recipe> actual, MessageContext context) = await sut.Hero.Crafting.Recipes.GetRecipesByIngredientItemIdByPage(visionCrystal, 0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.Links).IsNotNull();
        await Assert.That(context.PageSize).IsEqualTo(pageSize);
        await Assert.That(context.ResultCount).IsEqualTo(pageSize);
        await Assert.That(context.PageTotal > 0).IsTrue();
        await Assert.That(context.ResultTotal > 0).IsTrue();
        await Assert.That(actual.Count).IsEqualTo(pageSize);
        foreach (Recipe recipe in actual)
        {
            await Assert.That(recipe.Ingredients).Contains(ingredient => ingredient.Id == visionCrystal);
        }
    }
}
