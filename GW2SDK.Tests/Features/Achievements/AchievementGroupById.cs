using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Achievements;

public class AchievementGroupById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2";

        var actual = await sut.Achievements.GetAchievementGroupById(id);

        Assert.Equal(id, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_description();
        actual.Value.Has_order();
        actual.Value.Has_categories();
    }
}
