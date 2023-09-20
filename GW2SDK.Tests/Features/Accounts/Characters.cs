using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Accounts;

public class Characters
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Accounts.GetCharacters(accessToken.Key);

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }
}
