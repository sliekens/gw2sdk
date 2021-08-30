using System;
using System.Threading.Tasks;
using GW2SDK.Continents;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Continents
{
    public class ContinentServiceTest
    {
        private static class ContinentFact
        {
            public static void Id_is_1_or_2(Continent actual) => Assert.InRange(actual.Id, 1, 2);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_continents()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ContinentService>();

            var actual = await sut.GetContinents();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
            Assert.All(actual.Values,
                continent =>
                {
                    ContinentFact.Id_is_1_or_2(continent);
                });
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_continent_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ContinentService>();

            var actual = await sut.GetContinentsIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_a_continent_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            var actual = await sut.GetContinentById(continentId);

            Assert.Equal(continentId, actual.Value.Id);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_continents_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ContinentService>();

            var ids = new[] { 1, 2 };

            var actual = await sut.GetContinentsByIds(ids);

            Assert.Collection(actual.Values, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id));
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public async Task Continent_ids_cannot_be_null()
        {
            await using var services = new Composer();
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
            await using var services = new Composer();
            var sut = services.Resolve<ContinentService>();

            await Assert.ThrowsAsync<ArgumentException>("continentIds",
                async () =>
                {
                    await sut.GetContinentsByIds(Array.Empty<int>());
                });
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_continents_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ContinentService>();

            var actual = await sut.GetContinentsByPage(0, 2);

            Assert.Equal(2, actual.Values.Count);
            Assert.Equal(2, actual.Context.PageSize);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_floors_by_continent_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            var actual = await sut.GetFloors(continentId);

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_floor_ids_by_continent_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            var actual = await sut.GetFloorsIndex(continentId);

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_a_floor_by_continent_id_and_floor_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;
            const int floorId = 1;

            var actual = await sut.GetFloorById(continentId, floorId);

            Assert.Equal(floorId, actual.Value.Id);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_floors_by_continent_id_and_floor_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;
            var ids = new[] { 1, 2 };

            var actual = await sut.GetFloorsByIds(continentId, ids);

            Assert.Collection(actual.Values, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id));
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public async Task Floor_ids_cannot_be_null()
        {
            await using var services = new Composer();
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
            await using var services = new Composer();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            await Assert.ThrowsAsync<ArgumentException>("floorIds",
                async () =>
                {
                    await sut.GetFloorsByIds(continentId, Array.Empty<int>());
                });
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_floors_by_continent_id_and_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ContinentService>();

            const int continentId = 1;

            var actual = await sut.GetFloorsByPage(continentId, 0, 3);

            Assert.Equal(3, actual.Values.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }
    }
}
