using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.WizardsVault.Seasons;

public class Season
{

    [Test]

    public async Task Can_be_found()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (GuildWars2.WizardsVault.Seasons.Season actual, MessageContext context) = await sut.WizardsVault.GetSeason(cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotNull(context);

        Assert.NotEmpty(actual.Title);

        Assert.True(actual.Start <= DateTimeOffset.UtcNow);

        Assert.True(actual.End > actual.Start);

        Assert.NotEmpty(actual.AstralRewardIds);

        Assert.NotEmpty(actual.ObjectiveIds);
    }
}
