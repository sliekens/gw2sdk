using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.BuildStorage;

public class BuildStorageByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        HashSet<int> ids = new()
        {
            2,
            3,
            4
        };

        var actual = await sut.BuildStorage.GetBuildStorageSpacesByIds(accessToken.Key, ids);

        Assert.NotEmpty(actual.Value);
        Assert.All(
            actual.Value,
            space =>
            {
                Assert.NotEmpty(space.Name);
            }
        );
    }
}