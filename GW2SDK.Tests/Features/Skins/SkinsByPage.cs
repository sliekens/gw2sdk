using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Skins;

public class SkinsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Wardrobe.GetSkinsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.NotNull(actual.Context.PageContext);
        Assert.Equal(3, actual.Context.PageContext.PageSize);
    }
}
