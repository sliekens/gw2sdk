using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Wvw.Ranks;

namespace GuildWars2.Tests.Features.Wvw.Ranks;

public class Ranks
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<Rank> actual, MessageContext context) = await sut.Wvw.GetRanks(cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.NotEmpty(entry.Title);
            Assert.True(entry.MinRank > 0);
        });
    }
}
