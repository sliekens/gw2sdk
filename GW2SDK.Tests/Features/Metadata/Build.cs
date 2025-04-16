using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Metadata;

public class Build
{
    [Fact]
    public async Task Current_build_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, _) =
            await sut.Metadata.GetBuild(cancellationToken: TestContext.Current.CancellationToken);

        Assert.True(actual.Id > 115267);
    }
}
