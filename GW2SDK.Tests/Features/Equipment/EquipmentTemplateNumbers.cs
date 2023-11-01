using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Equipment;

public class EquipmentTemplateNumbers
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Equipment.GetEquipmentTemplateNumbers(character.Name, accessToken.Key);

        Assert.NotEmpty(actual.Value);
        Assert.NotNull(actual.ResultContext);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultCount);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultTotal);
    }
}
