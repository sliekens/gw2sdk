using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Files;

public class FileById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const string fileId = "map_vendor_ecto";

        var actual = await sut.Files.GetFileById(fileId);

        Assert.Equal(fileId, actual.Value.Id);
        actual.Value.Has_icon();
    }
}
