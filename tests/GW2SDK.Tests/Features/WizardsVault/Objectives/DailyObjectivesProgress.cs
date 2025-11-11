using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure;
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
        Assert.True(actual.RewardItemId > 0);
        Assert.True(actual.RewardAcclaim > 0);
        Assert.True(actual.Progress >= 0);
        Assert.True(actual.Goal >= 0);
        Assert.Equal(actual.Progress, actual.Objectives.Count(objective => objective.Claimed));
        Assert.NotEmpty(actual.Objectives);
        Assert.All(actual.Objectives, objective =>
        {
            Assert.True(objective.Id > 0);
            Assert.NotEmpty(objective.Title);
            Assert.True(objective.Track.IsDefined());
            Assert.True(objective.RewardAcclaim > 0);
        });
    }
}
