using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

[ServiceDataSource]
public class MountSkinsIndex(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<int> actual, MessageContext context) =
            await sut.Hero.Equipment.Mounts.GetMountSkinsIndex(
                TestContext.Current!.Execution.CancellationToken
            );

        // https://github.com/gw2-api/issues/issues/134
        Assert.Equal(context.ResultCount, actual.Count + 1);
        Assert.Equal(context.ResultTotal, actual.Count + 1);
        Assert.NotEmpty(actual);
    }
}
