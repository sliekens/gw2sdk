using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

public class LearnedRecipes
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = TestConfiguration.TestCharacter;
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) =
            await sut.Hero.Crafting.Recipes.GetLearnedRecipes(character.Name, accessToken.Key);

        Assert.NotEmpty(actual);
        Assert.All(actual, id => Assert.NotEqual(0, id));
    }
}
