using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Accounts;

[ServiceDataSource]
public class CharacterByName(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;
        (Character actual, _) = await sut.Hero.Account.GetCharacterByName(character.Name, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual.Name).IsEqualTo(character.Name);
        await Assert.That(actual.Race).IsEqualTo(character.Race);
        await Assert.That(actual.Profession).IsEqualTo(character.Profession);
    }
}
