using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.WizardsVault.Objectives;

namespace GuildWars2.Tests.Features.WizardsVault.Objectives;

[ServiceDataSource]
public class ObjectiveById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 1;
        (Objective actual, MessageContext context) = await sut.WizardsVault.GetObjectiveById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).IsNotNull();
            await Assert.That(actual.Id).IsEqualTo(id);
        }
    }
}
