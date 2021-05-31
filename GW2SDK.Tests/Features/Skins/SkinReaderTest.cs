using System.Text.Json;
using GW2SDK.Json;
using GW2SDK.Skins;
using GW2SDK.Tests.Features.Skins.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Skins
{
    public class SkinReaderTest : IClassFixture<SkinFixture>
    {
        public SkinReaderTest(SkinFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly SkinFixture _fixture;

        private static class SkinFact
        {
            public static void Id_is_positive(Skin actual) => Assert.InRange(actual.Id, 1, int.MaxValue);
        }

        [Fact]
        [Trait("Feature", "Skins")]
        [Trait("Category", "Integration")]
        [Trait("Importance", "Critical")]
        public void Skins_can_be_created_from_json()
        {
            var sut = new SkinReader();

            AssertEx.ForEach(_fixture.Skins,
                json =>
                {
                    using var document = JsonDocument.Parse(json);

                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

                    SkinFact.Id_is_positive(actual);
                });
        }
    }
}
