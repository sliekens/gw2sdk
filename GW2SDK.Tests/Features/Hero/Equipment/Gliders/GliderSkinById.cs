using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Gliders;

public class GliderSkinById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 58;

        var (actual, context) = await sut.Hero.Equipment.Gliders.GetGliderSkinById(id);

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
