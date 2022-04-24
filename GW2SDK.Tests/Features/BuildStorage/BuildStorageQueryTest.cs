using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.BuildStorage;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.BuildStorage;

public class BuildStorageQueryTest
{
    [Fact]
    public async Task Build_storage_index_is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<BuildStorageQuery>();
        var accessToken = services.Resolve<ApiKey>();

        var actual = await sut.GetBuildStorageIndex(accessToken.Key);

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task A_build_storage_space_can_be_found_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<BuildStorageQuery>();
        var accessToken = services.Resolve<ApiKey>();

        const int id = 3;

        var actual = await sut.GetBuildStorageSpaceById(accessToken.Key, id);

        Assert.NotNull(actual.Value);
        Assert.NotEmpty(actual.Value.Name);
    }

    [Fact]
    public async Task Build_storage_can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<BuildStorageQuery>();
        var accessToken = services.Resolve<ApiKey>();

        HashSet<int> ids = new()
        {
            2,
            3,
            4
        };

        var actual = await sut.GetBuildStorageSpacesByIds(accessToken.Key, ids);

        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            space =>
            {
                Assert.NotEmpty(space.Name);
            }
            );
    }

    [Fact]
    public async Task Build_storage_can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<BuildStorageQuery>();
        var accessToken = services.Resolve<ApiKey>();

        var actual = await sut.GetBuildStorageSpacesByPage(accessToken.Key, 0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }

    [Fact]
    public async Task Build_storage_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<BuildStorageQuery>();
        var accessToken = services.Resolve<ApiKey>();

        var actual = await sut.GetBuildStorage(accessToken.Key);

        Assert.NotEmpty(actual.Values);
        Assert.All(
            actual,
            space =>
            {
                Assert.NotEmpty(space.Name);
            }
            );
    }
}
