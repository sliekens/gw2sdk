using GuildWars2.Hero.StoryJournal.Stories;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

public class Stories
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        (HashSet<Story> actual, MessageContext context) = await sut.Hero.StoryJournal.GetStories(
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry.Id > 0);
                Assert.NotEmpty(entry.StorylineId);
                Assert.NotEmpty(entry.Name);
                Assert.NotNull(entry.Description);
                Assert.NotNull(entry.Timeline);
                Assert.True(entry.Level > 0);
                Assert.NotEmpty(entry.Races);
                Assert.True(entry.Order >= 0);
                Assert.NotNull(entry.Chapters);
                Assert.All(
                    entry.Chapters,
                    chapter =>
                    {
                        Assert.NotEmpty(chapter.Name);
                    }
                );
                Assert.Empty(entry.Flags.Other);
            }
        );
    }
}
