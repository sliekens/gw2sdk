using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Features.Worlds;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Shared;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Worlds
{
    public class WorldServiceTest
    {
        public WorldServiceTest(ITestOutputHelper output)
        {
            _output = output;
        }

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task GetWorldIds_ShouldReturnAllWorldIds()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new WorldService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetWorldIds(settings);

            Assert.NotEmpty(actual);
            Assert.Equal(actual.ResultCount, actual.Count);
            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task GetWorldById_ShouldReturnRequestedWorld()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new WorldService(http);

            const int worldId = 1001;

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetWorldById(worldId, settings);

            Assert.Equal(worldId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task GetWorldsByIds_ShouldReturnRequestedWorlds()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new WorldService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetWorldsByIds(new List<int> { 1001, 1002, 1003 }, settings);

            Assert.NotEmpty(actual);
            Assert.Collection(actual, world => Assert.Equal(1001, world.Id), world => Assert.Equal(1002, world.Id), world => Assert.Equal(1003, world.Id));
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Unit")]
        public async Task GetWorldsByIds_WithIdsNull_ShouldThrowArgumentNullException()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new WorldService(http);

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
            var http = HttpClientFactory.CreateDefault();

            var sut = new WorldService(http);

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
            var http = HttpClientFactory.CreateDefault();

            var sut = new WorldService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetWorlds(settings);

            Assert.NotEmpty(actual);
            Assert.Equal(actual.Count, actual.ResultTotal);
            Assert.Equal(actual.Count, actual.ResultCount);
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public async Task GetWorldsByPage_ShouldReturnAllWorlds()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new WorldService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetWorldsByPage(0, 200, settings);

            Assert.NotEmpty(actual);
            Assert.Equal(actual.Count, actual.ResultTotal);
            Assert.Equal(actual.Count, actual.ResultCount);
            Assert.Equal(200,          actual.PageSize);
            Assert.Equal(1,            actual.PageTotal);
        }
    }
}
