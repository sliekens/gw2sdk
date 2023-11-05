using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Equipment;

public class EquipmentTemplates
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Equipment.GetEquipmentTemplates(character.Name, accessToken.Key);

        Assert.NotEmpty(actual);
        Assert.All(actual, entry =>
        {
            Assert.NotNull(entry);
            Assert.NotEmpty(entry.Items);
            Assert.NotNull(entry.PvpEquipment);
        });
    }
}
