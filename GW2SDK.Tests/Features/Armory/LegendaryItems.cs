using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Armory;

public class LegendaryItems
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Armory.GetLegendaryItems();

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
