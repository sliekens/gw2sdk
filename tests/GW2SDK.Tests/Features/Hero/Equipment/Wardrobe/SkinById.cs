using GuildWars2.Hero.Equipment.Wardrobe;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Wardrobe;

public class SkinById
{
    [Test]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        const int id = 1;
        (EquipmentSkin actual, MessageContext context) = await sut.Hero.Equipment.Wardrobe.GetSkinById(id, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
