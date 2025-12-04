using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.WizardsVault.Objectives;

[ServiceDataSource]
public class SpecialObjectivesProgress(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (GuildWars2.WizardsVault.Objectives.SpecialObjectivesProgress actual, MessageContext context) = await sut.WizardsVault.GetSpecialObjectivesProgress(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).IsNotNull();
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
