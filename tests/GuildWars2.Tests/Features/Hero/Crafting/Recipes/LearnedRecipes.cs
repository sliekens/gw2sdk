using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

[ServiceDataSource]
public class LearnedRecipes(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;
        (IImmutableValueSet<int> actual, _) = await sut.Hero.Crafting.Recipes.GetLearnedRecipes(character.Name, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        foreach (int id in actual)
        {
            await Assert.That(id).IsNotEqualTo(0);
        }
    }
}
