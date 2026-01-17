using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.WizardsVault.AstralRewards;

namespace GuildWars2.Tests.Features.WizardsVault.AstralRewards;

[ServiceDataSource]
public class AstralRewardsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<int> ids = [1, 2, 3];
        (IImmutableValueSet<AstralReward> actual, MessageContext context) = await sut.WizardsVault.GetAstralRewardsByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).Count().IsEqualTo(ids.Count);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(ids.Count));
            await Assert.That(context.ResultTotal!.Value).IsGreaterThan(ids.Count);
            foreach (int id in ids)
            {
                await Assert.That(actual).Contains(found => found.Id == id);
            }
        }
    }
}
