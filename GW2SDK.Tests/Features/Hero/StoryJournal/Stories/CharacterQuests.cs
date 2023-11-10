using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

public class CharacterQuests
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Hero.StoryJournal.GetCharacterQuests(
            character.Name,
            accessToken.Key
        );

        Assert.NotEmpty(actual);
    }
}
