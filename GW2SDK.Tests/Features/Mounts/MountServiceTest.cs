using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Mounts;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Mounts
{
    public class MountServiceTest
    {
        [Fact]
        public async Task It_can_get_all_mounts()
        {
            await using var services = new Composer();
            var sut = services.Resolve<MountService>();

            var actual = await sut.GetMounts();

            Assert.Equal(Enum.GetNames(typeof(MountName)).Length, actual.Values.Count);

            foreach (var mount in actual.Values)
            {
                Assert.True(Enum.IsDefined(typeof(MountName), mount.Id), "Enum.IsDefined(mount.Id)");
                Assert.NotEmpty(mount.Name);
            }
        }

        [Fact]
        public async Task It_can_get_all_mount_names()
        {
            await using var services = new Composer();
            var sut = services.Resolve<MountService>();

            var actual = await sut.GetMountNames();

            Assert.Equal(Enum.GetNames(typeof(MountName)).Length, actual.Values.Count);
            Assert.All(actual.Values, name => Assert.True(Enum.IsDefined(typeof(MountName), name), "Enum.IsDefined(name)"));
        }

        [Fact]
        public async Task It_can_get_a_mount_by_name()
        {
            await using var services = new Composer();
            var sut = services.Resolve<MountService>();

            const MountName name = MountName.Skyscale;

            var actual = await sut.GetMountByName(name);

            Assert.Equal(name, actual.Value.Id);
        }

        [Fact]
        public async Task It_can_get_mounts_by_name()
        {
            await using var services = new Composer();
            var sut = services.Resolve<MountService>();

            var names = new[] { MountName.Raptor, MountName.Jackal, MountName.Skimmer };

            var actual = await sut.GetMountsByNames(names);

            Assert.Collection(actual.Values,
                first => Assert.Equal(names[0],  first.Id),
                second => Assert.Equal(names[1], second.Id),
                third => Assert.Equal(names[2],  third.Id));
        }

        [Fact]
        public async Task It_can_get_mounts_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<MountService>();

            var actual = await sut.GetMountsByPage(0, 3);

            Assert.Equal(3, actual.Values.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }

        [Fact]
        public async Task It_can_get_all_mount_skins()
        {
            await using var services = new Composer();
            var sut = services.Resolve<MountService>();

            var actual = await sut.GetMountSkins();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        public async Task It_can_get_all_mount_skin_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<MountService>();

            var actual = await sut.GetMountSkinsIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        public async Task It_can_get_a_mount_skin_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<MountService>();

            const int mountSkinId = 1;

            var actual = await sut.GetMountSkinById(mountSkinId);

            Assert.Equal(mountSkinId, actual.Value.Id);
        }

        [Fact]
        public async Task It_can_get_mount_skins_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<MountService>();

            var ids = new HashSet<int> { 1, 2, 3 };

            var actual = await sut.GetMountSkinsByIds(ids);

            Assert.Collection(actual.Values, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id), third => Assert.Equal(3, third.Id));
        }

        [Fact]
        public async Task It_can_get_mount_skins_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<MountService>();

            var actual = await sut.GetMountSkinsByPage(0, 3);

            Assert.Equal(3, actual.Values.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }
    }
}
