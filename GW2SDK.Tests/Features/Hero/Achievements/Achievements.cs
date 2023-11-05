using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Achievements;

public class Achievements
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        // You wouldn't want to use Take() in production code
        //   but enumerating all entries is too expensive for a test
        // This code will actually try to fetch more than 600 entries
        //  but the extra requests will be cancelled when this test completes
        await foreach (var (actual, context) in sut.Hero.Achievements.GetAchievementsBulk(degreeOfParallelism: 3).Take(600))
        {
            actual.Has_id();
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
            Assert.NotNull(context);
        }
    }
}
