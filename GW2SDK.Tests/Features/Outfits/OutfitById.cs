using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Outfits;

public class OutfitById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int outfitId = 1;

        var actual = await sut.Outfits.GetOutfitById(outfitId);

        Assert.Equal(outfitId, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_icon();
        actual.Value.Has_unlock_items();
    }
}
