using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.WizardsVault.AstralRewards;

namespace GuildWars2.Tests.Features.WizardsVault.AstralRewards;

public class AstralRewardById
{
    [Test]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        const int id = 1;
        (AstralReward actual, MessageContext context) = await sut.WizardsVault.GetAstralRewardById(id, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
