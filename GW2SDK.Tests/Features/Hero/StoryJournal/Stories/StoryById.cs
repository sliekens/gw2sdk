using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

public class StoryById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 63;

        var (actual, _) = await sut.Hero.StoryJournal.GetStoryById(id);

        Assert.Equal(id, actual.Id);
    }
}
