using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Wardrobe;

public class SkinById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var (actual, _) = await sut.Hero.Equipment.Wardrobe.GetSkinById(id);
        var link = actual.GetChatLink();

        Assert.Equal(id, actual.Id);
        Assert.Equal(id, link.SkinId);
    }
}
