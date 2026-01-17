using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

[ServiceDataSource]
public class RecipesIndexByIngredient(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int visionCrystalItemId = 46746;
        (IImmutableValueSet<int> actual, MessageContext context) = await sut.Hero.Crafting.Recipes.GetRecipesIndexByIngredientItemId(visionCrystalItemId, TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        await Assert.That(actual).IsNotEmpty();
        const int oneOfTheExpectedRecipeIds = 12316;
        await Assert.That(actual).Contains(oneOfTheExpectedRecipeIds);
    }
}
