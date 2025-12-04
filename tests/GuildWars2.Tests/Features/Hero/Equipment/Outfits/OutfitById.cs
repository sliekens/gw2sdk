using GuildWars2.Hero.Equipment.Outfits;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Outfits;

[ServiceDataSource]
public class OutfitById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 1;
        (Outfit actual, MessageContext context) = await sut.Hero.Equipment.Outfits.GetOutfitById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
