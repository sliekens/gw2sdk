using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.WizardsVault.Objectives;

[ServiceDataSource]
public class SpecialObjectivesProgress(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (GuildWars2.WizardsVault.Objectives.SpecialObjectivesProgress actual, MessageContext context) = await sut.WizardsVault.GetSpecialObjectivesProgress(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
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
