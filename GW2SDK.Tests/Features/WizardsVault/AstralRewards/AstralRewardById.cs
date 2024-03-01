using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.WizardsVault;

namespace GuildWars2.Tests.Features.WizardsVault.AstralRewards;

public class AstralRewardById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var (actual, _) = await sut.WizardsVault.GetAstralRewardById(id);

        Assert.Equal(id, actual.Id);
        Assert.True(actual.ItemId > 0);
        Assert.True(actual.ItemCount > 0);
        Assert.True(actual.Cost > 0);
        Assert.True(Enum.IsDefined(typeof(RewardKind), actual.Kind));

        var chatLink = actual.GetChatLink();
        Assert.Equal(actual.ItemId, chatLink.ItemId);
        Assert.Equal(actual.ItemCount, chatLink.Count);
    }
}
