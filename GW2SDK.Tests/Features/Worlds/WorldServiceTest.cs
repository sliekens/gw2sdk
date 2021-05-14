using System;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Worlds;
using Xunit;

namespace GW2SDK.Tests.Features.Worlds
{
    public class WorldServiceTest
    {
        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_worlds()
        {
            await using var services = new Composer();
            var sut = services.Resolve<WorldService>();

            var actual = await sut.GetWorlds();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_world_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<WorldService>();

            var actual = await sut.GetWorldsIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_a_world_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<WorldService>();

            const int worldId = 1001;

            var actual = await sut.GetWorldById(worldId);

            Assert.Equal(worldId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_worlds_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<WorldService>();

            var ids = new[] { 1001, 1002, 1003 };

            var actual = await sut.GetWorldsByIds(ids);

            Assert.Collection(actual, world => Assert.Equal(1001, world.Id), world => Assert.Equal(1002, world.Id), world => Assert.Equal(1003, world.Id));
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Unit")]
        public async Task World_ids_cannot_be_null()
        {
            await using var services = new Composer();
            var sut = services.Resolve<WorldService>();

            await Assert.ThrowsAsync<ArgumentNullException>("worldIds",
                async () =>
                {
                    await sut.GetWorldsByIds(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Unit")]
        public async Task World_ids_cannot_be_empty()
        {
            await using var services = new Composer();
            var sut = services.Resolve<WorldService>();

            await Assert.ThrowsAsync<ArgumentException>("worldIds",
                async () =>
                {
                    await sut.GetWorldsByIds(new int[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_worlds_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<WorldService>();

            var actual = await sut.GetWorldsByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task Page_index_cannot_be_negative()
        {
            await using var services = new Composer();
            var sut = services.Resolve<WorldService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetWorldsByPage(-1, 3));
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task Page_size_cannot_be_negative()
        {
            await using var services = new Composer();
            var sut = services.Resolve<WorldService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetWorldsByPage(1, -3));
        }
    }
}
