using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Accounts;

public class CharacterByName
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Accounts.GetCharacterByName(character.Name, accessToken.Key);

        Assert.Equal(character.Name, actual.Value.Name);
        Assert.Equal(character.Race, actual.Value.Race);
        Assert.Equal(character.Profession, actual.Value.Profession);
    }
}
