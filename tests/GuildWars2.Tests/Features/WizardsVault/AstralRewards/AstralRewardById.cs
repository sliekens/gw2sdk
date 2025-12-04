using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.WizardsVault.AstralRewards;

namespace GuildWars2.Tests.Features.WizardsVault.AstralRewards;

[ServiceDataSource]
public class AstralRewardById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 1;
        (AstralReward actual, MessageContext context) = await sut.WizardsVault.GetAstralRewardById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).IsNotNull();
            await Assert.That(actual.Id).IsEqualTo(id);
        }
    }
}
