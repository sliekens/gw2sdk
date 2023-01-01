using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Skins;

public class WardrobeQueryTest
{
    [Fact]
    public async Task Skins_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Wardrobe.GetSkinsIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task A_skin_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int skinId = 1;

        var actual = await sut.Wardrobe.GetSkinById(skinId);

        Assert.Equal(skinId, actual.Value.Id);
    }

    [Fact]
    public async Task Skins_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Wardrobe.GetSkinsByIds(ids);

        Assert.Collection(
            actual.Value,
            skin => Assert.Equal(1, skin.Id),
            skin => Assert.Equal(2, skin.Id),
            skin => Assert.Equal(3, skin.Id)
        );
    }

    [Fact]
    public async Task Skins_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Wardrobe.GetSkinsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }
}
