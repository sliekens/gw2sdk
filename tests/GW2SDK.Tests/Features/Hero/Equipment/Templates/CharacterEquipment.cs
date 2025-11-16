using System.Text.Json;

using GuildWars2.Hero.Equipment.Templates;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

[ServiceDataSource]
public class CharacterEquipmentByName(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;
        (CharacterEquipment actual, MessageContext context) = await sut.Hero.Equipment.Templates.GetCharacterEquipment(character.Name, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.NotNull(actual);
        Assert.NotNull(actual.Items);
        Assert.All(actual.Items, EquipmentItemValidation.Validate);
#if NET
        string json = JsonSerializer.Serialize(actual, Common.TestJsonContext.Default.CharacterEquipment);
        CharacterEquipment? roundtrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.CharacterEquipment);
#else
        string json = JsonSerializer.Serialize(actual);
        CharacterEquipment? roundtrip = JsonSerializer.Deserialize<CharacterEquipment>(json);
#endif
        Assert.Equal(actual, roundtrip);
    }
}
