using GuildWars2.Hero.Crafting.Recipes;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

[ServiceDataSource]
public class RecipesByOutputItem(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int ironIngot = 19683;
        (HashSet<Recipe> actual, _) = await sut.Hero.Crafting.Recipes.GetRecipesByOutputItemId(ironIngot, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        const int ironIngotRecipe = 19;
        Assert.Contains(actual, recipe => recipe.Id == ironIngotRecipe);
    }
}
