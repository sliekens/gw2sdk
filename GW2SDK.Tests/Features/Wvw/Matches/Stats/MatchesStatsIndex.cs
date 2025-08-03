using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Matches.Stats;

public class MatchesStatsIndex
{
    [Fact]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<string> actual, MessageContext context) =
            await sut.Wvw.GetMatchesStatsIndex(TestContext.Current.CancellationToken);

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
    }
}
