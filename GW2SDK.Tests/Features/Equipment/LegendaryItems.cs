using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Equipment;

public class LegendaryItems
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Equipment.GetLegendaryItems();

        Assert.NotNull(actual.ResultContext);
        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
            entry =>
            {
                Assert.True(entry.Id > 0);
                Assert.True(entry.MaxCount > 0);
            }
        );
    }
}
