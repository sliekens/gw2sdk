using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.WizardsVault.Objectives;

public class SpecialObjectivesProgress
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
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
