using System.Text.Json;
using GW2SDK.Json;
using GW2SDK.Tests.Features.Traits.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Traits;
using Xunit;

namespace GW2SDK.Tests.Features.Traits
{
    public class TraitReaderTest : IClassFixture<TraitsFixture>
    {
        public TraitReaderTest(TraitsFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly TraitsFixture _fixture;

        private static class TraitFact
        {
            public static void Id_is_positive(Trait actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

        }

        [Fact]
        [Trait("Feature",    "Traits")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Traits_can_be_created_from_json()
        {
            var sut = new TraitReader();

            AssertEx.ForEach(_fixture.Traits,
                json =>
                {
                    using var document = JsonDocument.Parse(json);

                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

                    TraitFact.Id_is_positive(actual);
                });
        }
    }
}
