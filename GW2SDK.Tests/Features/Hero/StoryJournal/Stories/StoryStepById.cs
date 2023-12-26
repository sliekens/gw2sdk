using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

public class StoryStepById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 15;

        var (actual, _) = await sut.Hero.StoryJournal.GetStoryStepById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_name();
        actual.Has_level();
        actual.Has_story();
        actual.Has_goals();
    }
}
