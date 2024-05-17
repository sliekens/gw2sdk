using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

public class CompletedStorySteps
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = TestConfiguration.TestCharacter;
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Hero.StoryJournal.GetCompletedStorySteps(
            character.Name,
            accessToken.Key
        );

        Assert.NotEmpty(actual);
    }
}
