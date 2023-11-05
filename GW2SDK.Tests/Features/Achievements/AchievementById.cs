using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Achievements;

public class AchievementById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var (actual, _) = await sut.Achievements.GetAchievementById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_name();
        actual.Has_description();
        actual.Has_requirement();
        actual.Has_LockedText();
        actual.Has_flags();
        actual.Has_tiers();
        actual.Tiers_does_not_contain_null();
        actual.Rewards_does_not_contain_null();
        actual.Bits_does_not_contain_null();
        actual.PointCap_is_negative_1_for_repeatable_achievements_without_points();
    }
}
