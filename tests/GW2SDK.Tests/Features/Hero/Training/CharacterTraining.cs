using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Training;

[ServiceDataSource]
public class CharacterTraining(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;
        (GuildWars2.Hero.Training.CharacterTraining actual, _) = await sut.Hero.Training.GetCharacterTraining(character.Name, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        // BUG: currently this data is unavailable :(
        // Change this back to Assert.NotEmpty once fixed
        // https://github.com/gw2-api/issues/issues/56
        Assert.Empty(actual.Training);
        Assert.All(actual.Training, entry => Assert.NotEqual(0, entry.Spent));
        Assert.All(actual.Training, entry => Assert.True(entry.Done));
    }
}
