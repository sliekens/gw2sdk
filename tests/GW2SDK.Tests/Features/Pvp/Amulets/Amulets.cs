using GuildWars2.Pvp.Amulets;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.Amulets;

[ServiceDataSource]
public class Amulets(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Amulet> actual, MessageContext context) = await sut.Pvp.GetAmulets(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.NotEmpty(entry.Name);
            Assert.NotNull(entry.IconUrl);
            Assert.NotEmpty(entry.Attributes);
        });
    }
}
