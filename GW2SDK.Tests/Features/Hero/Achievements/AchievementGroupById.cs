using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Achievements;

public class AchievementGroupById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2";

        var (actual, context) = await sut.Hero.Achievements.GetAchievementGroupById(id);

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
