using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Raids;

public class RaidById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const string id = "forsaken_thicket";

        var actual = await sut.Raids.GetRaidById(id);

        Assert.Equal(id, actual.Value.Id);
    }
}
