using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Items.Stats;

public class ItemStatsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Items.GetItemStatsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.NotNull(context.PageContext);
        Assert.Equal(3, context.PageContext.PageSize);
        Assert.False(context.PageContext.Next.IsEmpty);
    }
}
