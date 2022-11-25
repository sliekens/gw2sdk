using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Stories;

public class SeasonById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const string id = "09766A86-D88D-4DF2-9385-259E9A8CA583";

        var actual = await sut.Stories.GetSeasonById(id);

        Assert.Equal(id, actual.Value.Id);
    }
}
