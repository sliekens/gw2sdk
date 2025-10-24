using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

public class LearnedRecipes
{

    [Test]

    public async Task Can_be_found()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        TestCharacter character = TestConfiguration.TestCharacter;

        ApiKey accessToken = TestConfiguration.ApiKey;

        (HashSet<int> actual, _) = await sut.Hero.Crafting.Recipes.GetLearnedRecipes(character.Name, accessToken.Key, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotEmpty(actual);

        Assert.All(actual, id => Assert.NotEqual(0, id));
    }
}
