using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.WizardsVault.Seasons;

public class Season
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, _) = await sut.WizardsVault.GetSeason();

        Assert.NotEmpty(actual.Title);
        Assert.True(actual.Start <= DateTimeOffset.UtcNow);
        Assert.True(actual.End > actual.Start);
        Assert.NotEmpty(actual.AstralRewardIds);
        Assert.NotEmpty(actual.ObjectiveIds);
    }
}
