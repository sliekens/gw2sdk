using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.BuildStorage;

public class BuildStorageById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        const int id = 3;

        var actual = await sut.BuildStorage.GetBuildStorageSpaceById(accessToken.Key, id);

        Assert.NotNull(actual.Value);
        Assert.NotEmpty(actual.Value.Name);
    }
}
