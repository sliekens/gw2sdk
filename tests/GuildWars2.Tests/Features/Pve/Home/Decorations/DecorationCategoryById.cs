using GuildWars2.Pve.Home.Decorations;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Home.Decorations;

[ServiceDataSource]
public class DecorationCategoryById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 1;
        (DecorationCategory actual, MessageContext context) = await sut.Pve.Home.GetDecorationCategoryById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
