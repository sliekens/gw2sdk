using GW2SDK.Tests.Features.Traits.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Traits;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Traits
{
    public class TraitTest : IClassFixture<TraitsFixture>
    {
        public TraitTest(TraitsFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly TraitsFixture _fixture;

        private readonly ITestOutputHelper _output;

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
            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(_output)
                .ThrowErrorOnMissingMember()
                .Build();

            AssertEx.ForEach(_fixture.Traits,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Trait>(json, settings);
                    TraitFact.Id_is_positive(actual);
                });
        }
    }
}
