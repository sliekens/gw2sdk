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
        Assert.NotNull(context.Links);
        Assert.Equal(50, context.PageSize);
        Assert.Equal(1, context.PageTotal);
        Assert.Equal(context.ResultTotal, context.ResultCount);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.TabNumber > 0);
            Assert.NotNull(entry.Name);
            Assert.NotNull(entry.Items);
            Assert.All(entry.Items, EquipmentItemValidation.Validate);
            Assert.NotNull(entry.PvpEquipment);
            PvpEquipmentValidation.Validate(entry.PvpEquipment);
#if NET
            string json = JsonSerializer.Serialize(entry, GuildWars2JsonContext.Default.EquipmentTemplate);
            EquipmentTemplate? roundtrip = JsonSerializer.Deserialize(json, GuildWars2JsonContext.Default.EquipmentTemplate);
#else
            string json = JsonSerializer.Serialize(entry);
            EquipmentTemplate? roundtrip = JsonSerializer.Deserialize<EquipmentTemplate>(json);
#endif
            Assert.Equal(entry, roundtrip);
        });
    }
}
