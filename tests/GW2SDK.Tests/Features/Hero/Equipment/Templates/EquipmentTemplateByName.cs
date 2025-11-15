using System.Text.Json;

using GuildWars2.Hero.Equipment.Templates;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

[ServiceDataSource]
public class EquipmentTemplateByName(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;
        const int tab = 1;
        (EquipmentTemplate actual, MessageContext context) = await sut.Hero.Equipment.Templates.GetEquipmentTemplate(character.Name, tab, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.True(actual.TabNumber > 0);
        Assert.NotEmpty(actual.Name);
        Assert.NotEmpty(actual.Items);
        Assert.All(actual.Items, EquipmentItemValidation.Validate);
        Assert.NotNull(actual.PvpEquipment);
        PvpEquipmentValidation.Validate(actual.PvpEquipment);
#if NET
        string json = JsonSerializer.Serialize(actual, GuildWars2JsonContext.Default.EquipmentTemplate);
        EquipmentTemplate? roundtrip = JsonSerializer.Deserialize(json, GuildWars2JsonContext.Default.EquipmentTemplate);
#else
        string json = JsonSerializer.Serialize(actual);
        EquipmentTemplate? roundtrip = JsonSerializer.Deserialize<EquipmentTemplate>(json);
#endif
        Assert.Equal(actual, roundtrip);
    }
}
