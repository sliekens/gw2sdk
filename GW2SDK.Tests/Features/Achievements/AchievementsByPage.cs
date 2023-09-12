using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Achievements;

public class AchievementsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int pageSize = 3;
        var actual = await sut.Achievements.GetAchievementsByPage(0, pageSize);

        Assert.Equal(pageSize, actual.Value.Count);
        Assert.Equal(pageSize, actual.PageContext.PageSize);
        Assert.Equal(pageSize, actual.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
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
