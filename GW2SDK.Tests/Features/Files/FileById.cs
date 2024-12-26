using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Files;

public class FileById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "map_vendor_ecto";

        var (actual, context) = await sut.Files.GetFileById(id, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
