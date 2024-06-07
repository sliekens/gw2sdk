using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.WizardsVault.Objectives;

public class SpecialObjectivesProgress
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, context) =
            await sut.WizardsVault.GetSpecialObjectivesProgress(accessToken.Key);

        Assert.NotNull(context);
        Assert.NotEmpty(actual.Objectives);
        Assert.All(
            actual.Objectives,
            objective =>
            {
                Assert.True(objective.Id > 0);
                Assert.NotEmpty(objective.Title);
                Assert.True(objective.Track.IsDefined());
                Assert.True(objective.RewardAcclaim > 0);
            }
        );
    }
}
