using System.Text.Json;
using GW2SDK.Continents;
using GW2SDK.Json;
using GW2SDK.Tests.Features.Continents.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Continents
{
    [Collection(nameof(ContinentDbCollection))]
    public class ContinentReaderTest
    {
        public ContinentReaderTest(ContinentFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ContinentFixture _fixture;

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
            var sut = new ContinentReader();

            AssertEx.ForEach(_fixture.Db.Continents,
                json =>
                {
                    using var document = JsonDocument.Parse(json);

                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

                    ContinentFact.Id_is_1_or_2(actual);
                });
        }
    }
}
