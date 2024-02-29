using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Achievements;

public class TitlesByFilter
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

        var (actual, context) = await sut.Hero.Achievements.GetTitlesByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(ids.Count, context.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                Assert.Contains(entry.Id, ids);
                entry.Has_name();
                entry.Can_be_unlocked_by_achievements();
            }
        );
    }
}
