using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Equipment;

public class CharacterEquipment
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Equipment.GetCharacterEquipment(character.Name, accessToken.Key);

        Assert.NotNull(actual.Value);
    }
}
