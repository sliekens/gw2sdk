using System.Text.Json;

using GuildWars2.Hero.Equipment.Templates;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

[ServiceDataSource]
public class ActiveEquipmentTemplate(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;
        (EquipmentTemplate actual, MessageContext context) = await sut.Hero.Equipment.Templates.GetActiveEquipmentTemplate(character.Name, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual).IsNotNull()
            .And.Member(a => a.TabNumber, tabNumber => tabNumber.IsGreaterThan(0))
            .And.Member(a => a.Name, name => name.IsNotEmpty())
            .And.Member(a => a.Items, items => items.IsNotEmpty());
        using (Assert.Multiple())
        {
            foreach (EquipmentItem item in actual.Items)
            {
                await EquipmentItemValidation.Validate(item);
            }
        }
        await Assert.That(actual.PvpEquipment).IsNotNull();
        await PvpEquipmentValidation.Validate(actual.PvpEquipment);
#if NET
        string json = JsonSerializer.Serialize(actual, Common.TestJsonContext.Default.EquipmentTemplate);
        EquipmentTemplate? roundtrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.EquipmentTemplate);
#else
        string json = JsonSerializer.Serialize(actual);
        EquipmentTemplate? roundtrip = JsonSerializer.Deserialize<EquipmentTemplate>(json);
#endif
        await Assert.That(roundtrip).IsEqualTo(actual);
    }
}
