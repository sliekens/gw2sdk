using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.WorldBosses;

public class WorldBosses
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Pve.WorldBosses.GetWorldBosses(cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
    }
}
