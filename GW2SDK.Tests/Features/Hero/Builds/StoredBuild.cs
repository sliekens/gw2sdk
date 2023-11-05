using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class StoredBuild
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        const int slotNumber = 3;

        var (actual, _) = await sut.Hero.Builds.GetStoredBuild(slotNumber, accessToken.Key);

        Assert.NotNull(actual);
        Assert.NotEmpty(actual.Name);
    }
}
