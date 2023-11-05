using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Achievements;

public class AchievementGroupById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2";

        var (actual, _) = await sut.Achievements.GetAchievementGroupById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_name();
        actual.Has_description();
        actual.Has_order();
        actual.Has_categories();
    }
}
