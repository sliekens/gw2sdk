using GuildWars2.Hero.Builds;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Builds;

[ServiceDataSource]
public class Legends(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Legend> actual, MessageContext context) = await sut.Hero.Builds.GetLegends(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.NotEmpty(entry.Id);
            Assert.True(entry.Code > 0);
        });
    }
}
