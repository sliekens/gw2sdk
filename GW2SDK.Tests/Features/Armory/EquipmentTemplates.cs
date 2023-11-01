using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Armory;

public class EquipmentTemplates
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Armory.GetEquipmentTemplates(character.Name, accessToken.Key);

        Assert.NotEmpty(actual.Value);
        Assert.All(actual.Value, entry =>
        {
            Assert.NotNull(entry);
            Assert.NotEmpty(entry.Items);
            Assert.NotNull(entry.PvpEquipment);
        });
    }
}
