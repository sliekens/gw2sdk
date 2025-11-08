using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

public class RecipesIndexByIngredient
{
    [Test]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        const int visionCrystalItemId = 46746;
        (HashSet<int> actual, MessageContext context) = await sut.Hero.Crafting.Recipes.GetRecipesIndexByIngredientItemId(visionCrystalItemId, TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        const int oneOfTheExpectedRecipeIds = 12316;
        Assert.Contains(oneOfTheExpectedRecipeIds, actual);
    }
}
