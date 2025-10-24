using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

public class EquipmentTemplateNumbers
{

    [Test]

    public async Task Can_be_listed()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        TestCharacter character = TestConfiguration.TestCharacter;

        ApiKey accessToken = TestConfiguration.ApiKey;

        (IReadOnlyList<int> actual, MessageContext context) = await sut.Hero.Equipment.Templates.GetEquipmentTemplateNumbers(character.Name, accessToken.Key, TestContext.Current!.CancellationToken);

        Assert.NotEmpty(actual);

        Assert.Equal(context.ResultCount, actual.Count);

        Assert.Equal(context.ResultTotal, actual.Count);
    }
}
