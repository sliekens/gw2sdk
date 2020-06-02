using GW2SDK.Impl.JsonConverters;
using GW2SDK.Skins;
using GW2SDK.Tests.Features.Skins.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Skins
{
    [Collection(nameof(SkinDbCollection))]
    public class SkinTest
    {
        public SkinTest(SkinFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly SkinFixture _fixture;

        private readonly ITestOutputHelper _output;

        private static class SkinFact
        {
            public static void Id_is_positive(Skin actual) => Assert.InRange(actual.Id, 1, int.MaxValue);
        }

        [Fact]
        [Trait("Feature",    "Skins")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Skins_can_be_created_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .Build();
            AssertEx.ForEach(_fixture.Db.Skins,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Skin>(json, settings);

                    SkinFact.Id_is_positive(actual);
                });
        }
    }
}
