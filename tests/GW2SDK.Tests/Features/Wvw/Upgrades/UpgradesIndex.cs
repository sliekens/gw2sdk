using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Wvw.Upgrades;

public class UpgradesIndex
{

    [Test]

    public async Task Can_be_listed()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<int> actual, MessageContext context) = await sut.Wvw.GetUpgradesIndex(TestContext.Current!.CancellationToken);

        Assert.Equal(context.ResultCount, actual.Count);

        Assert.Equal(context.ResultTotal, actual.Count);

        Assert.NotEmpty(actual);
    }
}
