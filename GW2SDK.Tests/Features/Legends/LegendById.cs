using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Legends;

public class LegendById
{
    [Theory]
    [InlineData("Legend1")]
    [InlineData("Legend2")]
    [InlineData("Legend3")]
    public async Task Can_be_found(string legendId)
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Legends.GetLegendById(legendId);

        Assert.Equal(legendId, actual.Value.Id);
        actual.Value.Has_code();
    }
}
