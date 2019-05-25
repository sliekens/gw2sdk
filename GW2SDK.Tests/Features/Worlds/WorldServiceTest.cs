using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Worlds;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Worlds
{
    public class WorldServiceTest : IClassFixture<HttpFixture>
    {
        public WorldServiceTest(HttpFixture http, ITestOutputHelper output)
        {
            _http = http;
            _output = output;
        }

        private readonly HttpFixture _http;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature", "Worlds")]
        [Trait("Category", "E2E")]
        public async Task GetWorldIds_ShouldReturnAllWorldIds()
        {
            var sut = new WorldService(_http.Http);

            var actual = await sut.GetWorldIds();

            _output.WriteLine("GetWorldIds: {0}", actual.ToCsv());

            Assert.NotEmpty(actual);
            Assert.Equal(actual.ResultCount, actual.Count);
            Assert.Equal(actual.ResultTotal, actual.Count);
        }


        [Fact]
        [Trait("Feature", "Worlds")]
        [Trait("Category", "E2E")]
        public async Task GetWorldById_ShouldReturnRequestedWorld()
        {
            var sut = new WorldService(_http.Http);

            const int input = 1001;

            var actual = await sut.GetWorldById(input);

            Assert.NotNull(actual);
            Assert.Equal(input, actual.Id);
        }

        [Fact]
        [Trait("Feature", "Worlds")]
        [Trait("Category", "E2E")]
        public async Task GetWorldsById_ShouldReturnRequestedWorlds()
        {
            var sut = new WorldService(_http.Http);

            var actual = await sut.GetWorldsById(new List<int> {1001, 1002, 1003});

            Assert.NotEmpty(actual);
            Assert.Collection(actual,
                world => Assert.Equal(1001, world.Id),
                world => Assert.Equal(1002, world.Id),
                world => Assert.Equal(1003, world.Id));
        }

        [Fact]
        [Trait("Feature", "Worlds")]
        [Trait("Category", "E2E")]
        public async Task GetAllWorlds_ShouldReturnAllWorlds()
        {
            var sut = new WorldService(_http.Http);

            var actual = await sut.GetAllWorlds();

            Assert.NotEmpty(actual);
            Assert.Equal(actual.Count, actual.ResultTotal);
            Assert.Equal(actual.Count, actual.ResultCount);
        }

        [Fact]
        [Trait("Feature", "Worlds")]
        [Trait("Category", "E2E")]
        public async Task GetWorldsByPage_ShouldReturnAllWorlds()
        {
            var sut = new WorldService(_http.Http);

            var actual = await sut.GetWorldsByPage(0, 200);

            Assert.NotEmpty(actual);
            Assert.Equal(actual.Count, actual.ResultTotal);
            Assert.Equal(actual.Count, actual.ResultCount);
            Assert.Equal(200, actual.PageSize);
            Assert.Equal(1, actual.PageTotal);
        }
    }
}
