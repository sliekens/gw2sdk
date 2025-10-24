using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class MountSkinsIndex
{

    [Test]

    public async Task Can_be_listed()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<int> actual, MessageContext context) = await sut.Hero.Equipment.Mounts.GetMountSkinsIndex(TestContext.Current!.CancellationToken);

        Assert.Equal(context.ResultCount, actual.Count);

        Assert.Equal(context.ResultTotal, actual.Count);

        Assert.NotEmpty(actual);
    }
}
