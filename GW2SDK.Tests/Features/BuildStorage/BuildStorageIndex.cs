using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.BuildStorage;

public class BuildStorageIndex
{
    [Fact]
    public async Task Is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.BuildStorage.GetBuildStorageIndex(accessToken.Key);

        Assert.NotEmpty(actual.Value);
        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }
}
