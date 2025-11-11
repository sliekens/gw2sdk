using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Accounts;

[ServiceDataSource]
public class CharacterNames(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<string> actual, MessageContext context) = await sut.Hero.Account.GetCharactersIndex(accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        string expected = TestConfiguration.TestCharacter.Name;
        Assert.Contains(expected, actual);
    }
}
