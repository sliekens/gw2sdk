using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Achievements;

public class AchievementById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var (actual, context) = await sut.Hero.Achievements.GetAchievementById(id);

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
