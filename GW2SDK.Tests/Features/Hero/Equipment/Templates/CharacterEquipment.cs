using System.Text.Json;

using GuildWars2.Hero.Equipment.Templates;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

public class CharacterEquipmentByName
{
    [Fact]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;

        (CharacterEquipment actual, MessageContext context) = await sut.Hero.Equipment.Templates.GetCharacterEquipment(
            character.Name,
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.NotNull(actual);
        Assert.NotNull(actual.Items);
        Assert.All(actual.Items, EquipmentItemValidation.Validate);

        string json = JsonSerializer.Serialize(actual);
        CharacterEquipment? roundtrip = JsonSerializer.Deserialize<CharacterEquipment>(json);
        Assert.Equal(actual, roundtrip);
    }
}
