using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.WorldBosses;

public class WorldBosses
{
    [Fact]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<string> actual, MessageContext context) =
            await sut.Pve.WorldBosses.GetWorldBosses(TestContext.Current.CancellationToken);

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
    }
}
