using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Equipment;

public class EquipmentTemplate
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        const int tab = 1;
        var actual = await sut.Equipment.GetEquipmentTemplate(character.Name, tab, accessToken.Key);

        Assert.NotNull(actual.Value);
        Assert.NotEmpty(actual.Value.Items);
        Assert.NotNull(actual.Value.PvpEquipment);
    }
}
