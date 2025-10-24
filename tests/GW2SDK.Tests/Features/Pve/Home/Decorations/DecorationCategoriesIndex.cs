using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Pve.Home.Decorations;

public class DecorationCategoriesIndex
{

    [Test]

    public async Task Can_be_listed()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<int> actual, MessageContext context) = await sut.Pve.Home.GetDecorationCategoriesIndex(TestContext.Current!.CancellationToken);

        Assert.Equal(context.ResultCount, actual.Count);

        Assert.Equal(context.ResultTotal, actual.Count);

        Assert.NotEmpty(actual);
    }
}
