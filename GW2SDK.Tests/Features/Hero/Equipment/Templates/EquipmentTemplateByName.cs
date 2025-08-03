using System.Text.Json;

using GuildWars2.Hero.Equipment.Templates;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

public class EquipmentTemplateByName
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;

        const int tab = 1;
        (EquipmentTemplate actual, MessageContext context) = await sut.Hero.Equipment.Templates.GetEquipmentTemplate(
            character.Name,
            tab,
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.True(actual.TabNumber > 0);
        Assert.NotEmpty(actual.Name);
        Assert.NotEmpty(actual.Items);
        Assert.All(actual.Items, EquipmentItemValidation.Validate);
        Assert.NotNull(actual.PvpEquipment);
        PvpEquipmentValidation.Validate(actual.PvpEquipment);

        var json = JsonSerializer.Serialize(actual);
        var roundtrip = JsonSerializer.Deserialize<EquipmentTemplate>(json);
        Assert.Equal(actual, roundtrip);
    }
}
