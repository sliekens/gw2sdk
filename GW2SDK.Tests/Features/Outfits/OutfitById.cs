using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Outfits;

public class OutfitById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int outfitId = 1;

        var actual = await sut.Outfits.GetOutfitById(outfitId);

        Assert.Equal(outfitId, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_icon();
        actual.Value.Has_unlock_items();
    }
}
