using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.WizardsVault.AstralRewards;

public class AstralRewards
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.WizardsVault.GetAstralRewards(cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            reward =>
            {
                Assert.True(reward.Id > 0);
                Assert.True(reward.ItemId > 0);
                Assert.True(reward.ItemCount > 0);
                Assert.True(reward.Cost > 0);
                Assert.True(reward.Kind.IsDefined());

                var chatLink = reward.GetChatLink();
                Assert.Equal(reward.ItemId, chatLink.ItemId);
                Assert.Equal(reward.ItemCount, chatLink.Count);
            }
        );
    }
}
