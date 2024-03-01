using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.WizardsVault;

namespace GuildWars2.Tests.Features.WizardsVault;

public class AstralRewardsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids =
        [
            1, 2,
            3
        ];

        var (actual, context) = await sut.WizardsVault.GetAstralRewardsByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.All(
            actual,
            reward =>
            {
                Assert.True(reward.Id > 0);
                Assert.True(reward.ItemId > 0);
                Assert.True(reward.ItemCount > 0);
                Assert.True(reward.Cost > 0);
                Assert.True(Enum.IsDefined(typeof(RewardKind), reward.Kind));

                var chatLink = reward.GetChatLink();
                Assert.Equal(reward.ItemId, chatLink.ItemId);
                Assert.Equal(reward.ItemCount, chatLink.Count);
            }
        );
    }
}
