using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.BuildStorage;

public class StoredBuild
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        const int slotNumber = 3;

        var actual = await sut.BuildStorage.GetStoredBuild(slotNumber, accessToken.Key);

        Assert.NotNull(actual.Value);
        Assert.NotEmpty(actual.Value.Name);
    }
}
