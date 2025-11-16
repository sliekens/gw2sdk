using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.WizardsVault.AstralRewards;

namespace GuildWars2.Tests.Features.WizardsVault.AstralRewards;

[ServiceDataSource]
public class AstralRewardsByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        const int pageSize = 3;
        (HashSet<AstralReward> actual, MessageContext context) = await sut.WizardsVault.GetAstralRewardsByPage(0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).HasCount().EqualTo(pageSize);
        using (Assert.Multiple())
        {
            await Assert.That(context.Links).IsNotNull();
            await Assert.That(context).Member(c => c.PageSize, ps => ps.IsEqualTo(pageSize));
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(pageSize));
            await Assert.That(context.PageTotal!.Value).IsGreaterThan(0);
            await Assert.That(context.ResultTotal!.Value).IsGreaterThan(0);
            foreach (AstralReward entry in actual)
            {
                await Assert.That(entry).IsNotNull();
            }
        }
    }
}
