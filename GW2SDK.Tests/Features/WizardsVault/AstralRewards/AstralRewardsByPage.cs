using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.WizardsVault;

namespace GuildWars2.Tests.Features.WizardsVault;

public class AstralRewardsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int pageSize = 3;
        var (actual, context) = await sut.WizardsVault.GetAstralRewardsByPage(0, pageSize);

        Assert.Equal(pageSize, actual.Count);
        Assert.NotNull(context.Links);
        Assert.Equal(pageSize, context.PageSize);
        Assert.Equal(context.ResultCount, pageSize);
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
