using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Achievements;

public class AchievementsByFilter
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

        var (actual, context) = await sut.Hero.Achievements.GetAchievementsByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.NotNull(context.ResultContext);
        Assert.Equal(ids.Count, context.ResultContext.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                Assert.Contains(entry.Id, ids);
                entry.Has_name();
                entry.Has_description();
                entry.Has_requirement();
                entry.Has_LockedText();
                entry.Has_flags();
                entry.Has_tiers();
                entry.Tiers_does_not_contain_null();
                entry.Rewards_does_not_contain_null();
                entry.Bits_does_not_contain_null();
                entry.PointCap_is_negative_1_for_repeatable_achievements_without_points();
            }
        );
    }
}
