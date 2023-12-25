using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.BackgroundStories;

public class BackgroundStoryQuestionsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids =
        [
            7, 10,
            11
        ];

        var (actual, _) = await sut.Hero.StoryJournal.GetBackgroundStoryQuestionsByIds(ids);

        Assert.Collection(
            ids,
            first => Assert.Contains(actual, found => found.Id == first),
            second => Assert.Contains(actual, found => found.Id == second),
            third => Assert.Contains(actual, found => found.Id == third)
        );
    }
}
