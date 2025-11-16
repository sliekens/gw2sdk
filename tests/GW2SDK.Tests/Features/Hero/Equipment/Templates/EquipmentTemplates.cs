using System.Text.Json;

using GuildWars2.Hero.Equipment.Templates;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

[ServiceDataSource]
public class EquipmentTemplates(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<EquipmentTemplate> actual, MessageContext context) = await sut.Hero.Equipment.Templates.GetEquipmentTemplates(character.Name, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.Links).IsNotNull();
        await Assert.That(context).Member(c => c.PageSize, pageSize => pageSize.IsEqualTo(50))
            .And.Member(c => c.PageTotal, pageTotal => pageTotal.IsEqualTo(1))
            .And.Member(c => c.ResultTotal, resultTotal => resultTotal.IsEqualTo(context.ResultCount))
            .And.Member(c => c.ResultCount, resultCount => resultCount.IsEqualTo(actual.Count));
        await Assert.That(actual).IsNotEmpty();
        using (Assert.Multiple())
        {
            foreach (EquipmentTemplate entry in actual)
            {
                await Assert.That(entry).Member(e => e.TabNumber, tabNumber => tabNumber.IsGreaterThan(0))
                    .And.Member(e => e.Name, name => name.IsNotNull())
                    .And.Member(e => e.Items, items => items.IsNotNull());
                using (Assert.Multiple())
                {
                    foreach (EquipmentItem item in entry.Items)
                    {
                        await EquipmentItemValidation.Validate(item);
                    }
                }
                await Assert.That(entry.PvpEquipment).IsNotNull();
                await PvpEquipmentValidation.Validate(entry.PvpEquipment);
#if NET
                string json = JsonSerializer.Serialize(entry, Common.TestJsonContext.Default.EquipmentTemplate);
                EquipmentTemplate? roundtrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.EquipmentTemplate);
#else
                string json = JsonSerializer.Serialize(entry);
                EquipmentTemplate? roundtrip = JsonSerializer.Deserialize<EquipmentTemplate>(json);
#endif
                await Assert.That(roundtrip).IsEqualTo(entry);
            }
        }
    }
}
