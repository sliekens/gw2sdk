using System.Collections.Generic;
using GW2SDK.Features.Common;
using GW2SDK.Features.Worlds;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Common;
using GW2SDK.Tests.Features.Worlds.Fixtures;
using GW2SDK.Tests.Shared;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Worlds
{
    public class WorldTest : IClassFixture<WorldFixture>
    {
        public WorldTest(WorldFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly WorldFixture _fixture;

        private readonly ITestOutputHelper _output;

        private IDataTransferList<World> CreateSut(JsonSerializerSettings jsonSerializerSettings)
        {
            var list = new List<World>();
            JsonConvert.PopulateObject(_fixture.JsonArrayOfWorlds, list, jsonSerializerSettings);
            return new DataTransferList<World>(list, _fixture.ListContext);
        }

        [Fact]
        [Trait("Feature", "Tokens")]
        [Trait("Category", "Integration")]
        [Trait("Importance", "Critical")]
        public void World_ShouldHaveNoMissingMembers()
        {
            _ = CreateSut(new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build());
        }

        [Fact]
        [Trait("Feature", "Tokens")]
        [Trait("Category", "Integration")]
        public void World_Id_ShouldBePositive()
        {
            var sut = CreateSut(new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build());

            Assert.All(sut,
                world => Assert.InRange(world.Id, 1, int.MaxValue));
        }

        [Fact]
        [Trait("Feature", "Tokens")]
        [Trait("Category", "Integration")]
        public void World_Name_ShouldNotBeEmpty()
        {
            var sut = CreateSut(new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build());

            Assert.All(sut,
                world => Assert.NotEmpty(world.Name));
        }
    }
}
