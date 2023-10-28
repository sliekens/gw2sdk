using System.Text.Json;
using GuildWars2.Achievements;

namespace GuildWars2.Tests.Features.Achievements;

public class AchievementJson : IClassFixture<AchievementFixture>
{
    public AchievementJson(AchievementFixture fixture)
    {
        this.fixture = fixture;
    }

    private readonly AchievementFixture fixture;

    [Fact]
    public void Achievements_can_be_created_from_json() =>
        Assert.All(
            fixture.Achievements,
            json =>
            {
                using var document = JsonDocument.Parse(json);

                var actual = document.RootElement.GetAchievement(MissingMemberBehavior.Error);

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
        );
}
