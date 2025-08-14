using GuildWars2.Chat;
using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.WizardsVault.AstralRewards;

namespace GuildWars2.Tests.Features.WizardsVault.AstralRewards;

public class PurchasedAstralRewards
{
    [Fact]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKey accessToken = TestConfiguration.ApiKey;

        (HashSet<PurchasedAstralReward> actual, MessageContext context) = await sut.WizardsVault.GetPurchasedAstralRewards(
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.Equal(context.ResultCount, actual.Count);

        Assert.All(
            actual,
            reward =>
            {
                Assert.True(reward.Id > 0);
                Assert.True(reward.ItemId > 0);
                Assert.True(reward.ItemCount > 0);
                Assert.True(reward.Cost > 0);
                Assert.True(reward.Kind.IsDefined());
                if (reward.PurchaseLimit.HasValue)
                {
                    Assert.NotNull(reward.Purchased);
                    Assert.True(reward.PurchaseLimit > 0);
                    Assert.InRange(reward.Purchased.Value, 0, reward.PurchaseLimit.Value);
                }
                else
                {
                    Assert.Null(reward.Purchased);
                }

                ItemLink chatLink = reward.GetChatLink();
                Assert.Equal(reward.ItemId, chatLink.ItemId);
                Assert.Equal(reward.ItemCount, chatLink.Count);
            }
        );
    }
}
