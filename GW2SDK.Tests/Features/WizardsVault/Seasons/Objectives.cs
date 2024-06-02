using GuildWars2.Http;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.WizardsVault.Seasons;

public class Season
{
    [Fact(Skip = "API returns an unknown error")]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.WizardsVault.GetSeason();

        Assert.NotNull(context);
        Assert.NotEmpty(actual.Title);
        Assert.True(actual.Start <= DateTimeOffset.UtcNow);
        Assert.True(actual.End > actual.Start);
        Assert.NotEmpty(actual.AstralRewardIds);
        Assert.NotEmpty(actual.ObjectiveIds);
    }

    // Test to prove that the API is misbehaving and the test above should be skipped
    // When this test is removed, the test above should be re-enabled by removing the Skip attribute
    [Fact]
    public async Task Returns_unknown_error()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var error = await Assert.ThrowsAsync<BadResponseException>(async () => await sut.WizardsVault.GetSeason());

        Assert.Equal("unknown error", error.Message);
    }
}
