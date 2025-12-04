using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.WizardsVault.Objectives;

[ServiceDataSource]
public class DailyObjectivesProgress(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (GuildWars2.WizardsVault.Objectives.DailyObjectivesProgress actual, MessageContext context) = await sut.WizardsVault.GetDailyObjectivesProgress(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(actual.RewardItemId).IsGreaterThan(0);
            await Assert.That(actual.RewardAcclaim).IsGreaterThan(0);
            await Assert.That(actual.Progress).IsGreaterThanOrEqualTo(0);
            await Assert.That(actual.Goal).IsGreaterThanOrEqualTo(0);
            await Assert.That(actual.Objectives.Count(objective => objective.Claimed)).IsEqualTo(actual.Progress);
            await Assert.That(actual.Objectives).IsNotEmpty();
            foreach (GuildWars2.WizardsVault.Objectives.ObjectiveProgress objective in actual.Objectives)
            {
                await Assert.That(objective.Id).IsGreaterThan(0);
                await Assert.That(objective.Title).IsNotEmpty();
                await Assert.That(objective.Track.IsDefined()).IsTrue();
                await Assert.That(objective.RewardAcclaim).IsGreaterThan(0);
            }
        }
    }
}
