using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Stories;

public class StoryById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int id = 63;

        var actual = await sut.Stories.GetStoryById(id);

        Assert.Equal(id, actual.Value.Id);
    }
}
