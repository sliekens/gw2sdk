using GuildWars2.Chat;
using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.WizardsVault.AstralRewards;

namespace GuildWars2.Tests.Features.WizardsVault.AstralRewards;

public class AstralRewards
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<AstralReward> actual, MessageContext context) = await sut.WizardsVault.GetAstralRewards(cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, reward =>
        {
            Assert.True(reward.Id > 0);
            Assert.True(reward.ItemId > 0);
            Assert.True(reward.ItemCount > 0);
            Assert.True(reward.Cost > 0);
            Assert.True(reward.Kind.IsDefined());
            ItemLink chatLink = reward.GetChatLink();
            Assert.Equal(reward.ItemId, chatLink.ItemId);
            Assert.Equal(reward.ItemCount, chatLink.Count);
        });
    }
}
