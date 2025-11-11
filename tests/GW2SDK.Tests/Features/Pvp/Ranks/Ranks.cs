using GuildWars2.Pvp.Ranks;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.Ranks;

[ServiceDataSource]
public class Ranks(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Rank> actual, MessageContext context) = await sut.Pvp.GetRanks(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.NotEmpty(entry.Name);
            Assert.NotNull(entry.IconUrl);
            // Assert.NotEmpty(entry.IconHref); // Obsolete, replaced by IconUrl
            Assert.NotEmpty(entry.Levels);
        });
    }
}
