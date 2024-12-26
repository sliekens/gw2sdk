using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

public class EquipmentTemplates
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = TestConfiguration.TestCharacter;
        var accessToken = TestConfiguration.ApiKey;

        var (actual, context) =
            await sut.Hero.Equipment.Templates.GetEquipmentTemplates(
                character.Name,
                accessToken.Key,
                cancellationToken: TestContext.Current.CancellationToken
            );

        Assert.NotNull(context.Links);
        Assert.Equal(50, context.PageSize);
        Assert.Equal(1, context.PageTotal);
        Assert.Equal(context.ResultTotal, context.ResultCount);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            entry =>
            {
                Assert.NotNull(entry);
                Assert.NotNull(entry.Items);
                Assert.NotNull(entry.PvpEquipment);
            }
        );
    }
}
