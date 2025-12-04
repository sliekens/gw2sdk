using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Matches.Scores;

namespace GuildWars2.Tests.Features.Wvw.Matches.Scores;

[ServiceDataSource]
public class MatchScoresByWorldId(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int worldId = 2006;
        (MatchScores actual, _) = await sut.Wvw.GetMatchScoresByWorldId(worldId, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotNull();
    }
}
