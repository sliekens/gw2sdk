using System.Collections.Generic;
using System.Threading.Tasks;

using GW2SDK.Skins;
using GW2SDK.Tests.TestInfrastructure;

using Xunit;

namespace GW2SDK.Tests.Features.Skins;

public class WardrobeTest
{
    [Fact]
    public async Task It_can_get_all_skin_ids()
    {
        await using Composer services = new();
        var sut = services.Resolve<Wardrobe>();

        var actual = await sut.GetSkinsIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task It_can_get_a_skin_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Wardrobe>();

        const int skinId = 1;

        var actual = await sut.GetSkinById(skinId);

        Assert.Equal(skinId, actual.Value.Id);
    }

    [Fact]
    public async Task It_can_get_skins_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Wardrobe>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.GetSkinsByIds(ids);

        Assert.Collection(actual,
            skin => Assert.Equal(1, skin.Id),
            skin => Assert.Equal(2, skin.Id),
            skin => Assert.Equal(3, skin.Id));
    }

    [Fact]
    public async Task It_can_get_skins_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<Wardrobe>();

        var actual = await sut.GetSkinsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }
}