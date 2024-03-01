using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.WizardsVault;

namespace GuildWars2.Tests.Features.WizardsVault.Objectives;

public class ObjectivesByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids =
        [
            1, 2,
            3
        ];

        var (actual, context) = await sut.WizardsVault.GetObjectivesByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(context.ResultCount, actual.Count);
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
