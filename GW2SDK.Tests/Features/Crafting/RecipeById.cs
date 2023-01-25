using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Crafting;

public class RecipeById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int recipeId = 1;

        var actual = await sut.Crafting.GetRecipeById(recipeId);

        Assert.Equal(recipeId, actual.Value.Id);
    }
}
