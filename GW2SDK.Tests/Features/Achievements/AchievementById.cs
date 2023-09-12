using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Achievements;

public class AchievementById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var actual = await sut.Achievements.GetAchievementById(id);

        Assert.Equal(id, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_description();
        actual.Value.Has_requirement();
        actual.Value.Has_LockedText();
        actual.Value.Has_flags();
        actual.Value.Has_tiers();
        actual.Value.Tiers_does_not_contain_null();
        actual.Value.Rewards_does_not_contain_null();
        actual.Value.Bits_does_not_contain_null();
        actual.Value.PointCap_is_negative_1_for_repeatable_achievements_without_points();
    }
}
