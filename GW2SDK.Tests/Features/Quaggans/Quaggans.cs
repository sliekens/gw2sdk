using GuildWars2.Quaggans;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Quaggans;

public class Quaggans
{
    [Fact]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<Quaggan> actual, MessageContext context) =
            await sut.Quaggans.GetQuaggans(
                cancellationToken: TestContext.Current.CancellationToken
            );

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.NotEmpty(entry.Id);
                Assert.NotNull(entry.ImageUrl);
                Assert.True(entry.ImageUrl.IsAbsoluteUri);
            }
        );
    }
}
