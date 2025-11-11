using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Matches.Scores;

namespace GuildWars2.Tests.Features.Wvw.Matches.Scores;

[ServiceDataSource]
public class MatchScoresById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "1-1";
        (MatchScores actual, MessageContext context) = await sut.Wvw.GetMatchScoresById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
