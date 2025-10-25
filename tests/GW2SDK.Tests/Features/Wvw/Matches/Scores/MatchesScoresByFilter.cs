using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Wvw.Matches.Scores;

namespace GuildWars2.Tests.Features.Wvw.Matches.Scores;

public class MatchesScoresByFilter
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        HashSet<string> ids = ["1-1", "1-2", "1-3"];
        (HashSet<MatchScores> actual, MessageContext context) = await sut.Wvw.GetMatchesScoresByIds(ids, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.Equal(ids.Count, context.ResultCount);
        Assert.True(context.ResultTotal > ids.Count);
        Assert.Equal(ids.Count, actual.Count);
        Assert.Collection(ids, first => Assert.Contains(actual, found => found.Id == first), second => Assert.Contains(actual, found => found.Id == second), third => Assert.Contains(actual, found => found.Id == third));
    }
}
