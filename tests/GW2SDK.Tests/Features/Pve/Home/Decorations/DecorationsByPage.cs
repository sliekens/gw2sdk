using GuildWars2.Pve.Home.Decorations;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Pve.Home.Decorations;

public class DecorationsByPage
{

    [Test]

    public async Task Can_be_filtered_by_page()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int pageSize = 3;

        (HashSet<Decoration> actual, MessageContext context) = await sut.Pve.Home.GetDecorationsByPage(0, pageSize, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotNull(context.Links);

        Assert.Equal(pageSize, context.PageSize);

        Assert.Equal(pageSize, context.ResultCount);

        Assert.True(context.PageTotal > 0);

        Assert.True(context.ResultTotal > 0);

        Assert.Equal(pageSize, actual.Count);

        Assert.All(actual, Assert.NotNull);
    }
}
