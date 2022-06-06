using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Quaggans;

public class QuagganById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const string quagganId = "present";

        var actual = await sut.Quaggans.GetQuagganById(quagganId);

        Assert.Equal(quagganId, actual.Value.Id);
        actual.Value.Quaggan_has_picture();
    }
}
