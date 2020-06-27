using GW2SDK.Continents;
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

        private static class ContinentFact
        {
            public static void Id_is_1_or_2(Continent actual) => Assert.InRange(actual.Id, 1, 2);
        }

        [Fact]
        [Trait("Feature",    "Continents")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Continents_can_be_created_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(_output)
                .ThrowErrorOnMissingMember()
                .Build();

            AssertEx.ForEach(_fixture.Db.Continents,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Continent>(json, settings);

                    ContinentFact.Id_is_1_or_2(actual);
                });
        }
    }
}
