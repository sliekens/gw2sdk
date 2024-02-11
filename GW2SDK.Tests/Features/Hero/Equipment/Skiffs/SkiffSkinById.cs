using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Skiffs;

public class SkiffSkinById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 410;

        var (actual, _) = await sut.Hero.Equipment.Skiffs.GetSkiffSkinById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_name();
        actual.Has_icon();
        actual.Has_dye_slots();
    }
}
