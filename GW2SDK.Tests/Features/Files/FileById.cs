using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Files;

public class FileById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string fileId = "map_vendor_ecto";

        var actual = await sut.Files.GetFileById(fileId);

        Assert.Equal(fileId, actual.Value.Id);
        actual.Value.Has_icon();
    }
}
