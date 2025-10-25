using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Wvw.Matches.Scores;

namespace GuildWars2.Tests.Features.Wvw.Matches.Scores;

public class MatchScoresByWorldId
{
    [Test]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        const int worldId = 2006;
        (MatchScores actual, _) = await sut.Wvw.GetMatchScoresByWorldId(worldId, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotNull(actual);
    }
}
