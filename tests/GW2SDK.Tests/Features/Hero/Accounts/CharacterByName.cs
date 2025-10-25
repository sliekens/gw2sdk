using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Accounts;

public class CharacterByName
{
    [Test]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;
        (Character actual, _) = await sut.Hero.Account.GetCharacterByName(character.Name, accessToken.Key, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.Equal(character.Name, actual.Name);
        Assert.Equal(character.Race, actual.Race);
        Assert.Equal(character.Profession, actual.Profession);
    }
}
