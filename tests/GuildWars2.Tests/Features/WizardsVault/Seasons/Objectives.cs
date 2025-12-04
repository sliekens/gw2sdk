using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.WizardsVault.Seasons;

[ServiceDataSource]
public class Season(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        (GuildWars2.WizardsVault.Seasons.Season actual, MessageContext context) = await sut.WizardsVault.GetSeason(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).IsNotNull();
            await Assert.That(actual.Title).IsNotEmpty();
            await Assert.That(actual.Start <= DateTimeOffset.UtcNow).IsTrue();
            await Assert.That(actual.End > actual.Start).IsTrue();
            await Assert.That(actual.AstralRewardIds).IsNotEmpty();
            await Assert.That(actual.ObjectiveIds).IsNotEmpty();
        }
    }
}
