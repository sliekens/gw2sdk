using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Achievements;

public class TitleById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var (actual, _) = await sut.Achievements.GetTitleById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_name();
        actual.Can_be_unlocked_by_achievements();
    }
}
