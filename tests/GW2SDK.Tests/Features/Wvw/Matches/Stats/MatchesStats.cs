using GuildWars2.Tests.TestInfrastructure;

using GuildWars2.Wvw.Matches.Stats;


namespace GuildWars2.Tests.Features.Wvw.Matches.Stats;

public class MatchesStats
{

    [Test]

    public async Task Can_be_listed()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<MatchStats> actual, MessageContext context) = await sut.Wvw.GetMatchesStats(cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotEmpty(actual);

        Assert.Equal(context.ResultCount, actual.Count);

        Assert.Equal(context.ResultTotal, actual.Count);

        Assert.All(actual, entry =>
        {
            Assert.NotEmpty(entry.Id);
            Assert.NotNull(entry.Kills);
            Assert.NotNull(entry.Deaths);
            Assert.NotEmpty(entry.Maps);
            Assert.All(entry.Maps, map =>
            {
                Assert.True(map.Id > 0);
                Assert.True(map.Kind.IsDefined());
            });
        });
    }
}
