using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Achievements;

public class AchievementsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Achievements.GetAchievementsByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(ids.Count, actual.Context.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
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
