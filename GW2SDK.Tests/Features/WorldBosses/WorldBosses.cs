using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.WorldBosses;

public class WorldBosses
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.WorldBosses.GetWorldBosses();
        
        Assert.NotEmpty(actual.Value);
        Assert.NotNull(actual.ResultContext);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultCount);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultTotal);
    }
}
