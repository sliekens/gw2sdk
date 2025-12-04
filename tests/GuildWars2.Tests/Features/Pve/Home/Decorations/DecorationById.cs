using GuildWars2.Pve.Home.Decorations;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Home.Decorations;

[ServiceDataSource]
public class DecorationById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 133;
        (Decoration actual, MessageContext context) = await sut.Pve.Home.GetDecorationById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
