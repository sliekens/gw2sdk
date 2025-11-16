using GuildWars2.Chat;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;
using GuildWars2.WizardsVault.AstralRewards;

namespace GuildWars2.Tests.Features.WizardsVault.AstralRewards;

[ServiceDataSource]
public class PurchasedAstralRewards(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<PurchasedAstralReward> actual, MessageContext context) = await sut.WizardsVault.GetPurchasedAstralRewards(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count));
            foreach (PurchasedAstralReward reward in actual)
            {
                await Assert.That(reward.Id).IsGreaterThan(0);
                await Assert.That(reward.ItemId).IsGreaterThan(0);
                await Assert.That(reward.ItemCount).IsGreaterThan(0);
                await Assert.That(reward.Cost).IsGreaterThan(0);
                await Assert.That(reward.Kind.IsDefined()).IsTrue();
                if (reward.PurchaseLimit.HasValue)
                {
                    await Assert.That(reward.PurchaseLimit.Value).IsGreaterThan(0);
                    int purchasedValue = await Assert.That(reward.Purchased).IsNotNull();
                    await Assert.That(purchasedValue).IsBetween(0, reward.PurchaseLimit.Value);
                }
                else
                {
                    await Assert.That(reward.Purchased).IsNull();
                }

                ItemLink chatLink = reward.GetChatLink();
                await Assert.That(chatLink.ItemId).IsEqualTo(reward.ItemId);
                await Assert.That(chatLink.Count).IsEqualTo(reward.ItemCount);
            }
        }
    }
}
