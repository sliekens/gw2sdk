using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Features.Worlds;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Shared;
using GW2SDK.Tests.Shared.Fixtures;
using Newtonsoft.Json;
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
        [Trait("Category", "Integration")]
        public async Task GetWorldIds_ShouldReturnAllWorldIds()
        {
            var sut = new WorldService(_http.Http);

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetWorldIds(settings);

            Assert.NotEmpty(actual);
            Assert.Equal(actual.ResultCount, actual.Count);
            Assert.Equal(actual.ResultTotal, actual.Count);
        }


        [Fact]
        [Trait("Feature", "Worlds")]
        [Trait("Category", "Integration")]
        public async Task GetWorldById_ShouldReturnRequestedWorld()
        {
            var sut = new WorldService(_http.Http);

            const int input = 1001;

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetWorldById(input, settings);

            Assert.NotNull(actual);
            Assert.Equal(input, actual.Id);
        }

        [Fact]
        [Trait("Feature", "Worlds")]
        [Trait("Category", "Integration")]
        public async Task GetWorldsById_ShouldReturnRequestedWorlds()
        {
            var sut = new WorldService(_http.Http);

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetWorldsById(new List<int> {1001, 1002, 1003}, settings);

            Assert.NotEmpty(actual);
            Assert.Collection(actual,
                world => Assert.Equal(1001, world.Id),
                world => Assert.Equal(1002, world.Id),
                world => Assert.Equal(1003, world.Id));
        }

        [Fact]
        [Trait("Feature", "Worlds")]
        [Trait("Category", "Integration")]
        public async Task GetAllWorlds_ShouldReturnAllWorlds()
        {
            var sut = new WorldService(_http.Http);

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAllWorlds(settings);

            Assert.NotEmpty(actual);
            Assert.Equal(actual.Count, actual.ResultTotal);
            Assert.Equal(actual.Count, actual.ResultCount);
        }

        [Fact]
        [Trait("Feature", "Worlds")]
        [Trait("Category", "Integration")]
        public async Task GetWorldsByPage_ShouldReturnAllWorlds()
        {
            var sut = new WorldService(_http.Http);

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetWorldsByPage(0, 200, settings);

            Assert.NotEmpty(actual);
            Assert.Equal(actual.Count, actual.ResultTotal);
            Assert.Equal(actual.Count, actual.ResultCount);
            Assert.Equal(200, actual.PageSize);
            Assert.Equal(1, actual.PageTotal);
        }

        [Fact]
        [Trait("Feature", "Worlds")]
        [Trait("Category", "Integration")]
        [Trait("Importance", "Critical")]
        public async Task World_ShouldHaveNoMissingMembers()
        {
            var sut = new WorldService(_http.Http);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            // Next statement throws if there are missing members
            _ = await sut.GetAllWorlds(settings);
        }

        [Fact]
        [Trait("Feature", "Worlds")]
        [Trait("Category", "Integration")]
        public async Task World_Id_ShouldBePositive()
        {
            var sut = new WorldService(_http.Http);

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAllWorlds(settings);

            Assert.All(actual,
                world => Assert.InRange(world.Id, 1, int.MaxValue));
        }

        [Fact]
        [Trait("Feature", "Worlds")]
        [Trait("Category", "Integration")]
        public async Task World_Name_ShouldNotBeEmpty()
        {
            var sut = new WorldService(_http.Http);

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAllWorlds(settings);

            Assert.All(actual,
                world => Assert.NotEmpty(world.Name));
        }
    }
}
