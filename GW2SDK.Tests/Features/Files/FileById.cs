using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Files;

public class FileById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "map_vendor_ecto";

        var (actual, _) = await sut.Files.GetFileById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_icon();
    }
}
