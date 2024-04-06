using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Skiffs;

public class SkiffSkinById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 410;

        var (actual, context) = await sut.Hero.Equipment.Skiffs.GetSkiffSkinById(id);

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
