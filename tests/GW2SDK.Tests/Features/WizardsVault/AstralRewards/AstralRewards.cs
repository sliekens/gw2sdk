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
        (HashSet<AstralReward> actual, MessageContext context) = await sut.WizardsVault.GetAstralRewards(cancellationToken: TestContext.Current!.Execution.CancellationToken);
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
