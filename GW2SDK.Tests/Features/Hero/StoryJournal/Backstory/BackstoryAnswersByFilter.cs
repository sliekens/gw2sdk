using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Backstory;

public class BackstoryAnswersByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids = new()
        {
            "7-53",
            "7-54",
            "7-55"
        };

        var (actual, _) = await sut.Hero.StoryJournal.GetBackstoryAnswersByIds(ids);

        Assert.Collection(
            ids,
            first => Assert.Contains(actual, found => found.Id == first),
            second => Assert.Contains(actual, found => found.Id == second),
            third => Assert.Contains(actual, found => found.Id == third)
        );
    }
}
