using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.MapChests;

public class MapChests
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Pve.MapChests.GetMapChests();

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.NotEmpty(entry.Id);
            }
        );
    }
}
