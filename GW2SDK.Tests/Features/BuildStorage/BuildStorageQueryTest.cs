using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.BuildStorage;

public class BuildStorageQueryTest
{
    [Fact]
    public async Task Build_storage_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.BuildStorage.GetBuildStorageIndex(accessToken.Key);

        Assert.NotEmpty(actual.Value);
        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task A_build_storage_space_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        const int id = 3;

        var actual = await sut.BuildStorage.GetBuildStorageSpaceById(accessToken.Key, id);

        Assert.NotNull(actual.Value);
        Assert.NotEmpty(actual.Value.Name);
    }

    [Fact]
    public async Task Build_storage_can_be_filtered_by_id()
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

    [Fact]
    public async Task Build_storage_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.BuildStorage.GetBuildStorageSpacesByPage(accessToken.Key, 0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }

    [Fact]
    public async Task Build_storage_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.BuildStorage.GetBuildStorage(accessToken.Key);

        Assert.NotEmpty(actual.Value);
        Assert.All(
            actual.Value,
            space =>
            {
                Assert.NotNull(space.Name);
                Assert.True(
                    Enum.IsDefined(typeof(ProfessionName), space.Profession),
                    "Enum.IsDefined(space.Profession)"
                );
                Assert.Equal(3, space.Specializations.Count);
                Assert.NotNull(space.Skills);
                Assert.NotNull(space.AquaticSkills);
            }
        );
    }
}
