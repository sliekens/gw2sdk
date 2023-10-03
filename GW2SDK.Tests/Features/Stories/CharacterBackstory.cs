using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Stories;

public class CharacterBackstory
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Stories.GetCharacterBackstory(
            character.Name,
            accessToken.Key
        );

        Assert.NotNull(actual.Value);
        Assert.NotEmpty(actual.Value.Backstory);
    }
}
