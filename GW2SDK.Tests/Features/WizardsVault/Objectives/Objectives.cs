using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.WizardsVault;

namespace GuildWars2.Tests.Features.WizardsVault.Objectives;

public class Objectives
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.WizardsVault.GetObjectives();

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Count, context.ResultCount);
        Assert.Equal(actual.Count, context.ResultTotal);
        Assert.All(
            actual,
            reward =>
            {
                Assert.True(reward.Id > 0);
                Assert.NotEmpty(reward.Title);
                Assert.True(Enum.IsDefined(typeof(ObjectiveTrack), reward.Track));
                Assert.True(reward.Acclaim > 0);
            }
        );
    }
}
