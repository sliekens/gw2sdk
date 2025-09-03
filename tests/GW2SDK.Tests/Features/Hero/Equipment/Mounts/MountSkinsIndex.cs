using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class MountSkinsIndex
{
    [Fact]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<int> actual, MessageContext context) =
            await sut.Hero.Equipment.Mounts.GetMountSkinsIndex(
                TestContext.Current.CancellationToken
            );

        // https://github.com/gw2-api/issues/issues/134
        Assert.Equal(context.ResultCount, actual.Count + 1);
        Assert.Equal(context.ResultTotal, actual.Count + 1);
        Assert.NotEmpty(actual);
    }
}
