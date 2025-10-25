using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Wvw.Matches.Overview;

namespace GuildWars2.Tests.Features.Wvw.Matches.Overview;

public class MatchesOverview
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<MatchOverview> actual, MessageContext context) = await sut.Wvw.GetMatchesOverview(cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.NotEmpty(entry.Id);
            Assert.True(entry.StartTime > DateTimeOffset.MinValue);
            Assert.True(entry.EndTime > entry.StartTime);
        });
    }
}
