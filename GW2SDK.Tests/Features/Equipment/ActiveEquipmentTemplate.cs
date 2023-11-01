using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Equipment;

public class ActiveEquipmentTemplate
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Equipment.GetActiveEquipmentTemplate(character.Name, accessToken.Key);

        Assert.NotNull(actual.Value);
        Assert.NotEmpty(actual.Value.Items);
        Assert.NotNull(actual.Value.PvpEquipment);
    }
}
