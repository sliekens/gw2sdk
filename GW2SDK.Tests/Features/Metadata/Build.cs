using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Metadata;

public class Build
{
    [Fact]
    public async Task Current_build_can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (GuildWars2.Metadata.Build actual, _) =
            await sut.Metadata.GetBuild(cancellationToken: TestContext.Current.CancellationToken);

        Assert.True(actual.Id > 115267);
    }
}
