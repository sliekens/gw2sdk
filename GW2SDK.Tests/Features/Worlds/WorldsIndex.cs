using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Worlds;

public class WorldsIndex
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Worlds.GetWorldsIndex();

        Assert.NotEmpty(actual);
        Assert.All(actual, id => Assert.True(id > 0));

        Assert.Equal(actual.Count, context.ResultCount);
        Assert.Equal(actual.Count, context.ResultTotal);
    }
}
