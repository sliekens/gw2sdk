using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

public class ActiveEquipmentTemplate
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Hero.Equipment.Equipment.GetActiveEquipmentTemplate(character.Name, accessToken.Key);

        Assert.NotNull(actual);
        Assert.NotEmpty(actual.Items);
        Assert.NotNull(actual.PvpEquipment);
    }
}
