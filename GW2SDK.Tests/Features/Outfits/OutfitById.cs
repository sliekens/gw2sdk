using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Outfits;

public class OutfitById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var actual = await sut.Outfits.GetOutfitById(id);

        Assert.Equal(id, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_icon();
        actual.Value.Has_unlock_items();
    }
}
