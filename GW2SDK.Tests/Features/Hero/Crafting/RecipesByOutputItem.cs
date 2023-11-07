using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Crafting;

public class RecipesByOutputItem
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int ironIngot = 19683;
        var (actual, _) = await sut.Hero.Crafting.GetRecipesByOutputItemId(ironIngot);

        const int ironIngotRecipe = 19;
        Assert.Contains(actual, recipe => recipe.Id == ironIngotRecipe);
    }
}
