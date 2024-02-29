using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Items.Stats;

public class AttributeCombinationsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Items.GetAttributeCombinationByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, context.PageSize);
        Assert.False(context.Links.Next.IsEmpty);
    }
}
