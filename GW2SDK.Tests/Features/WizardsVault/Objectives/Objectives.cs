using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.WizardsVault.Objectives;

public class Objectives
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.WizardsVault.GetObjectives(cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
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
