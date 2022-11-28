using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Quaggans;

public class Quaggans
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Quaggans.GetQuaggans();

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Count, actual.Context.ResultCount);
        Assert.Equal(actual.Count, actual.Context.ResultTotal);
        Assert.All(
            actual,
            entry =>
            {
                entry.Id_is_not_empty();
                entry.Quaggan_has_picture();
            }
        );
    }
}
