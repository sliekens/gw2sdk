using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Emblems;

public class ForegroundEmblemsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Emblems.GetForegroundEmblemsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.NotNull(context.PageContext);
        Assert.Equal(3, context.PageContext.PageSize);
    }
}
