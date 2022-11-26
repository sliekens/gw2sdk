using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Pvp.Amulets;

public class Amulets
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Pvp.GetAmulets();

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Count, actual.Context.ResultCount);
        Assert.Equal(actual.Count, actual.Context.ResultTotal);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_icon();
                entry.Has_attributes();
            }
        );
    }
}
