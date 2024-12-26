using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

public class ActiveEquipmentTemplate
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = TestConfiguration.TestCharacter;
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) =
            await sut.Hero.Equipment.Templates.GetActiveEquipmentTemplate(
                character.Name,
                accessToken.Key,
                cancellationToken: TestContext.Current.CancellationToken
            );

        Assert.NotNull(actual);
        Assert.NotEmpty(actual.Items);
        Assert.NotNull(actual.PvpEquipment);
    }
}
