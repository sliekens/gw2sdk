using GuildWars2.Hero.Equipment.Wardrobe;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Wardrobe;

[ServiceDataSource]
public class SkinById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 1;
        (EquipmentSkin actual, MessageContext context) = await sut.Hero.Equipment.Wardrobe.GetSkinById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
