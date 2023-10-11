using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Quests;

public class CharacterQuests
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Quests.GetCharacterQuests(
            character.Name,
            accessToken.Key
        );

        Assert.NotEmpty(actual.Value);
    }
}
