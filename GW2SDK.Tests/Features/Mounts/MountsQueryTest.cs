using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Mounts;

public class MountsQueryTest
{
    [Fact]
    public async Task Mounts_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Mounts.GetMounts();

        Assert.All(
            actual,
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
    public async Task Mount_names_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Mounts.GetMountNames();

        Assert.All(
            actual,
            name => Assert.True(Enum.IsDefined(typeof(MountName), name), "Enum.IsDefined(name)")
        );
    }

    [Fact]
    public async Task A_mount_can_be_found_by_name()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const MountName name = MountName.Skyscale;

        var actual = await sut.Mounts.GetMountByName(name);

        Assert.Equal(name, actual.Value.Id);
    }

    [Fact]
    public async Task Mounts_can_be_filtered_by_name()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        MountName[] names =
        {
            MountName.Raptor,
            MountName.Jackal,
            MountName.Skimmer
        };

        var actual = await sut.Mounts.GetMountsByNames(names);

        Assert.Collection(
            actual,
            first => Assert.Equal(names[0], first.Id),
            second => Assert.Equal(names[1], second.Id),
            third => Assert.Equal(names[2], third.Id)
        );
    }

    [Fact]
    public async Task Mounts_can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Mounts.GetMountsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }

    [Fact]
    public async Task Mount_skins_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Mounts.GetMountSkins();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task Mount_skins_index_is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Mounts.GetMountSkinsIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task A_mount_skin_can_be_found_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int mountSkinId = 1;

        var actual = await sut.Mounts.GetMountSkinById(mountSkinId);

        Assert.Equal(mountSkinId, actual.Value.Id);
    }

    [Fact]
    public async Task Mount_skins_can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Mounts.GetMountSkinsByIds(ids);

        Assert.Collection(
            actual,
            first => Assert.Equal(1, first.Id),
            second => Assert.Equal(2, second.Id),
            third => Assert.Equal(3, third.Id)
        );
    }

    [Fact]
    public async Task Mount_skins_can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Mounts.GetMountSkinsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }
}
