using System;
using System.Threading.Tasks;
using GW2SDK.Continents;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Continents
{
    public class ContinentServiceTest
    {
        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task GetContinents_ShouldReturnAllContinents()
        {
            var services = new Container();
            var sut = services.Resolve<ContinentService>();

            var actual = await sut.GetContinents();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task GetContinentsIndex_ShouldReturnAllIds()
        {
            var services = new Container();
            var sut = services.Resolve<ContinentService>();

            var actual = await sut.GetContinentsIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task GetContinentById_ShouldReturnThatContinent()
        {
            var services = new Container();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            var actual = await sut.GetContinentById(continentId);

            Assert.Equal(continentId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public async Task GetContinentsByIds_WithIdsNull_ShouldThrowArgumentNullException()
        {
            var services = new Container();
            var sut = services.Resolve<ContinentService>();

            await Assert.ThrowsAsync<ArgumentNullException>("continentIds",
                async () =>
                {
                    await sut.GetContinentsByIds(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public async Task GetContinentsByIds_WithIdsEmpty_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<ContinentService>();

            await Assert.ThrowsAsync<ArgumentException>("continentIds",
                async () =>
                {
                    await sut.GetContinentsByIds(new int[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task GetContinentsByIds_ShouldReturnThoseContinents()
        {
            var services = new Container();
            var sut = services.Resolve<ContinentService>();

            var ids = new[] { 1, 2 };

            var actual = await sut.GetContinentsByIds(ids);

            Assert.Collection(actual, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id));
        }
        
        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task GetContinentsByPage_WithInvalidPage_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<ContinentService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetContinentsByPage(-1, 3));
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task GetContinentsByPage_WithInvalidPageSize_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<ContinentService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetContinentsByPage(1, -3));
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task GetContinentsByPage_WithPage1AndPageSize3_ShouldReturnThatPage()
        {
            var services = new Container();
            var sut = services.Resolve<ContinentService>();

            var actual = await sut.GetContinentsByPage(0, 2);

            Assert.Equal(2, actual.Count);
            Assert.Equal(2, actual.PageSize);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task GetFloors_ShouldReturnAllFloors()
        {
            var services = new Container();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            var actual = await sut.GetFloors(continentId);

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task GetFloorsIndex_ShouldReturnAllIds()
        {
            var services = new Container();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            var actual = await sut.GetFloorsIndex(continentId);

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task GetFloorById_ShouldReturnThatFloor()
        {
            var services = new Container();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;
            const int floorId = 1;

            var actual = await sut.GetFloorById(continentId, floorId);

            Assert.Equal(floorId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public async Task GetFloorsByIds_WithIdsNull_ShouldThrowArgumentNullException()
        {
            var services = new Container();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            await Assert.ThrowsAsync<ArgumentNullException>("floorIds",
                async () =>
                {
                    await sut.GetFloorsByIds(continentId, null);
                });
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public async Task GetFloorsByIds_WithIdsEmpty_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            await Assert.ThrowsAsync<ArgumentException>("floorIds",
                async () =>
                {
                    await sut.GetFloorsByIds(continentId, new int[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task GetFloorsByIds_ShouldReturnThoseFloors()
        {
            var services = new Container();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;
            var ids = new[] { 1, 2 };

            var actual = await sut.GetFloorsByIds(continentId, ids);

            Assert.Collection(actual, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id));
        }
        
        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task GetFloorsByPage_WithInvalidPage_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetFloorsByPage(continentId, -1, 3));
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task GetFloorsByPage_WithInvalidPageSize_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetFloorsByPage(continentId, 1, -3));
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task GetFloorsByPage_WithPage1AndPageSize3_ShouldReturnThatPage()
        {
            var services = new Container();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            var actual = await sut.GetFloorsByPage(continentId, 0, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }
    }
}
