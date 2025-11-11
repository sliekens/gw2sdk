using GuildWars2.Quaggans;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Quaggans;

[ServiceDataSource]
public class Quaggans(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Quaggan> actual, MessageContext context) = await sut.Quaggans.GetQuaggans(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.NotEmpty(entry.Id);
            Assert.NotNull(entry.ImageUrl);
            Assert.True(entry.ImageUrl.IsAbsoluteUri);
        });
    }
}
