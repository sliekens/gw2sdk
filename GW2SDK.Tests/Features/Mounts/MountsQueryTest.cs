using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Mounts;

public class MountsQueryTest
{
    [Fact]
    public async Task Mounts_can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Mounts.GetMounts();

        Assert.All(
            actual.Value,
            mount =>
            {
                Assert.True(
                    Enum.IsDefined(typeof(MountName), mount.Id),
                    "Enum.IsDefined(mount.Id)"
                );
                Assert.NotEmpty(mount.Name);
            }
        );
    }

    [Fact]
    public async Task Mount_names_can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Mounts.GetMountNames();

        Assert.All(
            actual.Value,
            name => Assert.True(Enum.IsDefined(typeof(MountName), name), "Enum.IsDefined(name)")
        );
    }

    [Fact]
    public async Task A_mount_can_be_found_by_name()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const MountName name = MountName.Skyscale;

        var actual = await sut.Mounts.GetMountByName(name);

        Assert.Equal(name, actual.Value.Id);
    }

    [Fact]
    public async Task Mounts_can_be_filtered_by_name()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<MountName> names = new()
        {
            MountName.Raptor,
            MountName.Jackal,
            MountName.Skimmer
        };

        var actual = await sut.Mounts.GetMountsByNames(names);

        Assert.Collection(
            names,
            first => Assert.Contains(actual.Value, found => found.Id == first),
            second => Assert.Contains(actual.Value, found => found.Id == second),
            third => Assert.Contains(actual.Value, found => found.Id == third)
        );
    }

    [Fact]
    public async Task Mounts_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Mounts.GetMountsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }

    [Fact]
    public async Task Mount_skins_can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Mounts.GetMountSkins();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task Mount_skins_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Mounts.GetMountSkinsIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task A_mount_skin_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var actual = await sut.Mounts.GetMountSkinById(id);

        Assert.Equal(id, actual.Value.Id);
    }

    [Fact]
    public async Task Mount_skins_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Mounts.GetMountSkinsByIds(ids);

        Assert.Collection(
            ids,
            first => Assert.Contains(actual.Value, found => found.Id == first),
            second => Assert.Contains(actual.Value, found => found.Id == second),
            third => Assert.Contains(actual.Value, found => found.Id == third)
        );
    }

    [Fact]
    public async Task Mount_skins_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Mounts.GetMountSkinsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }

    [Fact]
    public async Task Owned_mounts_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Mounts.GetOwnedMounts(accessToken.Key);

        Assert.NotEmpty(actual.Value);
    }

    [Fact]
    public async Task Owned_mount_skins_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Mounts.GetOwnedMountSkins(accessToken.Key);

        Assert.NotEmpty(actual.Value);
    }
}
