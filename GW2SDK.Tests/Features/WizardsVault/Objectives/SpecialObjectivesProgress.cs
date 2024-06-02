using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.WizardsVault.Objectives;

public class SpecialObjectivesProgress
{
    [Fact(Skip = "API returns no results for the new season")]
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

    // Test to prove that the API is misbehaving and the test above should be skipped
    // When this test is removed, the test above should be re-enabled by removing the Skip attribute
    [Fact]
    public async Task Cannot_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, context) =
            await sut.WizardsVault.GetSpecialObjectivesProgress(accessToken.Key);

        Assert.NotNull(context);
        Assert.Empty(actual.Objectives);
    }
}
