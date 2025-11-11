using GuildWars2.Pve.Raids;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Raids;

[ServiceDataSource]
public class Raids(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Raid> actual, MessageContext context) = await sut.Pve.Raids.GetRaids(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.NotEmpty(entry.Id);
        });
    }
}
