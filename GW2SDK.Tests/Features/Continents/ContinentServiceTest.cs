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
        public async Task Get_all_continents()
        {
            await using var services = new Container();
            var sut = services.Resolve<ContinentService>();

            var actual = await sut.GetContinents();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task Get_all_continent_ids()
        {
            await using var services = new Container();
            var sut = services.Resolve<ContinentService>();

            var actual = await sut.GetContinentsIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task Get_a_continent_by_id()
        {
            await using var services = new Container();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            var actual = await sut.GetContinentById(continentId);

            Assert.Equal(continentId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task Get_continents_by_id()
        {
            await using var services = new Container();
            var sut = services.Resolve<ContinentService>();

            var ids = new[] { 1, 2 };

            var actual = await sut.GetContinentsByIds(ids);

            Assert.Collection(actual, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id));
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public async Task Continent_ids_cannot_be_null()
        {
            await using var services = new Container();
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
        public async Task Continent_ids_cannot_be_empty()
        {
            await using var services = new Container();
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
        public async Task Get_continents_by_page()
        {
            await using var services = new Container();
            var sut = services.Resolve<ContinentService>();

            var actual = await sut.GetContinentsByPage(0, 2);

            Assert.Equal(2, actual.Count);
            Assert.Equal(2, actual.PageSize);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task Continent_page_index_cannot_be_negative()
        {
            await using var services = new Container();
            var sut = services.Resolve<ContinentService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetContinentsByPage(-1, 3));
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task Continent_page_size_cannot_be_negative()
        {
            await using var services = new Container();
            var sut = services.Resolve<ContinentService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetContinentsByPage(1, -3));
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task Get_all_floors_by_continent_id()
        {
            await using var services = new Container();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            var actual = await sut.GetFloors(continentId);

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task Get_all_floor_ids_by_continent_id()
        {
            await using var services = new Container();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            var actual = await sut.GetFloorsIndex(continentId);

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task Get_a_floor_by_continent_id_and_floor_id()
        {
            await using var services = new Container();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;
            const int floorId = 1;

            var actual = await sut.GetFloorById(continentId, floorId);

            Assert.Equal(floorId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task Get_floors_by_continent_id_and_floor_ids()
        {
            await using var services = new Container();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;
            var ids = new[] { 1, 2 };

            var actual = await sut.GetFloorsByIds(continentId, ids);

            Assert.Collection(actual, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id));
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public async Task Floor_ids_cannot_be_null()
        {
            await using var services = new Container();
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
        public async Task Floor_ids_cannot_be_empty()
        {
            await using var services = new Container();
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
        public async Task Get_floors_by_continent_id_and_page()
        {
            await using var services = new Container();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            var actual = await sut.GetFloorsByPage(continentId, 0, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task Floor_page_index_cannot_be_negative()
        {
            await using var services = new Container();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetFloorsByPage(continentId, -1, 3));
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task Floor_page_size_cannot_be_negative()
        {
            await using var services = new Container();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetFloorsByPage(continentId, 1, -3));
        }
    }
}
