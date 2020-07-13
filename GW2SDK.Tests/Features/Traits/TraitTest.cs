using System.Linq;
using GW2SDK.Tests.Features.Traits.Fixtures;
using GW2SDK.Traits;
using GW2SDK.Traits.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Traits
{
    public class TraitTest : IClassFixture<TraitsFixture>
    {
        public TraitTest(TraitsFixture fixture)
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
            var sut = new TraitJsonReader();
            Assert.All(_fixture.Traits,
                json =>
                {
                    var actual = sut.Read(json.RootElement);

                    TraitFact.Id_is_positive(actual);
                });
        }
    }
}
