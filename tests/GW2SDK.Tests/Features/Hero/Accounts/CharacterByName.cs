using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Tests.TestInfrastructure.Composition;

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
        Assert.Equal(character.Name, actual.Name);
        Assert.Equal(character.Race, actual.Race);
        Assert.Equal(character.Profession, actual.Profession);
    }
}
