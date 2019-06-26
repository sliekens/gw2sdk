using System;
using System.Threading.Tasks;
using GW2SDK.Features.Worlds;
using Xunit;

namespace GW2SDK.Tests.Features.Worlds
{
    public class WorldServiceTest
    {
        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task GetWorlds_ShouldReturnAllWorlds()
        {
            var services = new Container();
            var sut = services.Resolve<WorldService>();

            var actual = await sut.GetWorlds();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task GetWorldsIndex_ShouldReturnAllIds()
        {
            var services = new Container();
            var sut = services.Resolve<WorldService>();

            var actual = await sut.GetWorldsIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task GetWorldById_ShouldReturnThatWorld()
        {
            var services = new Container();
            var sut = services.Resolve<WorldService>();

            const int worldId = 1001;

            var actual = await sut.GetWorldById(worldId);

            Assert.Equal(worldId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Unit")]
        public async Task GetWorldsByIds_WithIdsNull_ShouldThrowArgumentNullException()
        {
            var services = new Container();
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
        public async Task GetWorldsByIds_WithIdsEmpty_ShouldThrowArgumentException()
        {
            var services = new Container();
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
        public async Task GetWorldsByIds_ShouldReturnThoseWorlds()
        {
            var services = new Container();
            var sut = services.Resolve<WorldService>();

            var ids = new[] { 1001, 1002, 1003 };

            var actual = await sut.GetWorldsByIds(ids);

            Assert.Collection(actual, world => Assert.Equal(1001, world.Id), world => Assert.Equal(1002, world.Id), world => Assert.Equal(1003, world.Id));
        }
        
        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task GetWorldsByPage_WithInvalidPage_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<WorldService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetWorldsByPage(-1, 3));
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task GetWorldsByPage_WithInvalidPageSize_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<WorldService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetWorldsByPage(1, -3));
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task GetWorldsByPage_WithPage1AndPageSize3_ShouldReturnThatPage()
        {
            var services = new Container();
            var sut = services.Resolve<WorldService>();

            var actual = await sut.GetWorldsByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }
    }
}
