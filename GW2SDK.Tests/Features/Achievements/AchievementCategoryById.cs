using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Achievements;

public class AchievementCategoryById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var actual = await sut.Achievements.GetAchievementCategoryById(id);

        Assert.Equal(id, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_description();
        actual.Value.Has_order();
        actual.Value.Has_icon();
        actual.Value.Has_achievements();
    }

}
