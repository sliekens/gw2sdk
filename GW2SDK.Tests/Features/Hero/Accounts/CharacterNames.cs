using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Accounts;

public class CharacterNames
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Hero.Account.GetCharactersIndex(accessToken.Key);

        var expected = Composer.Resolve<TestCharacter>().Name;

        Assert.Contains(expected, actual);
    }
}
