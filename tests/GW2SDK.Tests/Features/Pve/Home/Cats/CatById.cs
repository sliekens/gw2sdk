using GuildWars2.Pve.Home.Cats;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Home.Cats;

[ServiceDataSource]
public class CatById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 20;
        (Cat actual, MessageContext context) = await sut.Pve.Home.GetCatById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
        await Assert.That(actual.Hint).IsEqualTo("necromancer");
    }
}
