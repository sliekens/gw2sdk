using GuildWars2.Chat;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.WizardsVault.AstralRewards;

namespace GuildWars2.Tests.Features.WizardsVault.AstralRewards;

[ServiceDataSource]
public class AstralRewards(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<AstralReward> actual, MessageContext context) = await sut.WizardsVault.GetAstralRewards(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count));
            await Assert.That(context).Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
            foreach (AstralReward reward in actual)
            {
                await Assert.That(reward.Id).IsGreaterThan(0);
                await Assert.That(reward.ItemId).IsGreaterThan(0);
                await Assert.That(reward.ItemCount).IsGreaterThan(0);
                await Assert.That(reward.Cost).IsGreaterThan(0);
                await Assert.That(reward.Kind.IsDefined()).IsTrue();
                ItemLink chatLink = reward.GetChatLink();
                await Assert.That(chatLink.ItemId).IsEqualTo(reward.ItemId);
                await Assert.That(chatLink.Count).IsEqualTo(reward.ItemCount);
            }
        }
    }
}
