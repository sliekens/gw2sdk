using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task GetWorldsIndex_ShouldReturnAllIds()
        {
            var services = new Container();
            var sut = services.Resolve<WorldService>();

            var actual = await sut.GetWorldsIndex();

            Assert.NotEmpty(actual);
            Assert.Equal(actual.ResultCount, actual.Count);
            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task GetWorldById_ShouldReturnRequestedWorld()
        {
            var services = new Container();
            var sut = services.Resolve<WorldService>();

            const int worldId = 1001;

            var actual = await sut.GetWorldById(worldId);

            Assert.Equal(worldId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task GetWorldsByIds_ShouldReturnRequestedWorlds()
        {
            var services = new Container();
            var sut = services.Resolve<WorldService>();

            var actual = await sut.GetWorldsByIds(new List<int> { 1001, 1002, 1003 });

            Assert.NotEmpty(actual);
            Assert.Collection(actual, world => Assert.Equal(1001, world.Id), world => Assert.Equal(1002, world.Id), world => Assert.Equal(1003, world.Id));
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
                    await sut.GetWorldsByIds(Enumerable.Empty<int>().ToList());
                });
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task GetWorlds_ShouldReturnAllWorlds()
        {
            var services = new Container();
            var sut = services.Resolve<WorldService>();

            var actual = await sut.GetWorlds();

            Assert.NotEmpty(actual);
            Assert.Equal(actual.Count, actual.ResultTotal);
            Assert.Equal(actual.Count, actual.ResultCount);
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task GetWorldsByPage_ShouldReturnAllWorlds()
        {
            var services = new Container();
            var sut = services.Resolve<WorldService>();

            var actual = await sut.GetWorldsByPage(0, 200);

            Assert.NotEmpty(actual);
            Assert.Equal(actual.Count, actual.ResultTotal);
            Assert.Equal(actual.Count, actual.ResultCount);
            Assert.Equal(200,          actual.PageSize);
            Assert.Equal(1,            actual.PageTotal);
        }
    }
}
