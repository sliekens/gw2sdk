using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Crafting;

public class RecipesByOutputItemByPage
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int ironIngot = 19683;
        var actual = await sut.Crafting.GetRecipesByOutputItemIdByPage(ironIngot, 0, 20);

        const int ironIngotRecipe = 19;
        Assert.Contains(actual.Value, recipe => recipe.Id == ironIngotRecipe);
    }
}
