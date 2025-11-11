using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.WizardsVault.Seasons;

[ServiceDataSource]
public class Season(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        (GuildWars2.WizardsVault.Seasons.Season actual, MessageContext context) = await sut.WizardsVault.GetSeason(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.NotEmpty(actual.Title);
        Assert.True(actual.Start <= DateTimeOffset.UtcNow);
        Assert.True(actual.End > actual.Start);
        Assert.NotEmpty(actual.AstralRewardIds);
        Assert.NotEmpty(actual.ObjectiveIds);
    }
}
