using GuildWars2.Pvp.Standings;
using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.Standings;

[ServiceDataSource]
public class Standings(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<Standing> actual, _) = await sut.Pvp.GetStandings(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
    }
}
