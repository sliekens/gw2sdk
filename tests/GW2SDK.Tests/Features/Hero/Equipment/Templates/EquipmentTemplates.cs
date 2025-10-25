using System.Text.Json;

using GuildWars2.Hero.Equipment.Templates;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

public class EquipmentTemplates
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<EquipmentTemplate> actual, MessageContext context) = await sut.Hero.Equipment.Templates.GetEquipmentTemplates(character.Name, accessToken.Key, cancellationToken: TestContext.Current!.CancellationToken);
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
            string json = JsonSerializer.Serialize(entry);
            EquipmentTemplate? roundtrip = JsonSerializer.Deserialize<EquipmentTemplate>(json);
            Assert.Equal(entry, roundtrip);
        });
    }
}
