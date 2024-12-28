using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class StoredBuild
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        const int slotNumber = 3;

        var (actual, _) = await sut.Hero.Builds.GetStoredBuild(
            slotNumber,
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(actual);
        Assert.NotEmpty(actual.Name);
    }
}
