using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Backstory;

public class BackstoryAnswerById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "7-53";

        var (actual, _) = await sut.Hero.StoryJournal.GetBackstoryAnswerById(id);

        Assert.Equal(id, actual.Id);
    }
}
