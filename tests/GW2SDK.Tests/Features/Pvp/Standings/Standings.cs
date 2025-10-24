using GuildWars2.Pvp.Standings;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Pvp.Standings;

public class Standings
{

    [Test]

    public async Task Can_be_listed()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        ApiKey accessToken = TestConfiguration.ApiKey;

        (HashSet<Standing> actual, _) = await sut.Pvp.GetStandings(accessToken.Key, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotEmpty(actual);
    }
}
