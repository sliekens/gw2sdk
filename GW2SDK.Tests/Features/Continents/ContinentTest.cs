using GW2SDK.Continents;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Tests.Features.Continents.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Continents
{
    [Collection(nameof(ContinentDbCollection))]
    public class ContinentTest
    {
        public ContinentTest(ContinentFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly ContinentFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",    "Continents")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Continents_can_be_serialized_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();

            AssertEx.ForEach(_fixture.Db.Continents,
                json =>
                {
                    // Next statement throws if there are missing members
                    _ = JsonConvert.DeserializeObject<Continent>(json, settings);
                });
        }
    }
}
