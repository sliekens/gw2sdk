using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.BackgroundStories;

public class CharacterBackgroundStory
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;

        (GuildWars2.Hero.StoryJournal.BackgroundStories.CharacterBackgroundStory actual, _) = await sut.Hero.StoryJournal.GetCharacterBackgroundStory(
            character.Name,
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(actual);
        Assert.NotEmpty(actual.AnswerIds);
    }
}
