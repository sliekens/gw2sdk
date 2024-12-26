using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Accounts;

public class CharacterNames
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, context) = await sut.Hero.Account.GetCharactersIndex(accessToken.Key, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);

        var expected = TestConfiguration.TestCharacter.Name;
        Assert.Contains(expected, actual);
    }
}
