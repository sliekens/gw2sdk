using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Pvp.Amulets;

public class AmuletsIndex
{
    [Fact]
    public async Task Is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Pvp.GetAmuletsIndex();

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Count, actual.Context.ResultCount);
        Assert.Equal(actual.Count, actual.Context.ResultTotal);
    }
}
