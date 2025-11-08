using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Wvw.Matches.Stats;

namespace GuildWars2.Tests.Features.Wvw.Matches.Stats;

public class MatchStatsByWorldId
{
    [Test]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        const int worldId = 2006;
        (MatchStats actual, _) = await sut.Wvw.GetMatchStatsByWorldId(worldId, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(actual);
    }
}
