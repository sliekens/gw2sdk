using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

public class EquipmentTemplates
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, context) =
            await sut.Hero.Equipment.Templates.GetEquipmentTemplates(
                character.Name,
                accessToken.Key
            );

        Assert.NotNull(context.Links);
        Assert.Equal(50, context.PageSize);
        Assert.InRange(context.ResultCount.GetValueOrDefault(), 1, 8);
        Assert.True(context.PageTotal > 0);
        Assert.True(context.ResultTotal > 0);

        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            entry =>
            {
                Assert.NotNull(entry);
                Assert.NotEmpty(entry.Items);
                Assert.NotNull(entry.PvpEquipment);
            }
        );
    }
}
