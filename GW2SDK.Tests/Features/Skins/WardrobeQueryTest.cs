using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Skins;

public class WardrobeQueryTest
{
    [Fact]
    public async Task Skins_index_is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<WardrobeQuery>();

        var actual = await sut.GetSkinsIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task A_skin_can_be_found_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<WardrobeQuery>();

        const int skinId = 1;

        var actual = await sut.GetSkinById(skinId);

        Assert.Equal(skinId, actual.Value.Id);
    }

    [Fact]
    public async Task Skins_can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<WardrobeQuery>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.GetSkinsByIds(ids);

        Assert.Collection(
            actual,
            skin => Assert.Equal(1, skin.Id),
            skin => Assert.Equal(2, skin.Id),
            skin => Assert.Equal(3, skin.Id)
            );
    }

    [Fact]
    public async Task Skins_can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<WardrobeQuery>();

        var actual = await sut.GetSkinsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }
}
