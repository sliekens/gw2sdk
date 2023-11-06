using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Stories;

public class CharacterBackstory
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Hero.Stories.GetCharacterBackstory(
            character.Name,
            accessToken.Key
        );

        Assert.NotNull(actual);
        Assert.NotEmpty(actual.Backstory);
    }
}
