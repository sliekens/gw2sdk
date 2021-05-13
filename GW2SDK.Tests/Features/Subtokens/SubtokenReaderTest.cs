using System.Text.Json;
using GW2SDK.Json;
using GW2SDK.Subtokens;
using GW2SDK.Tests.Features.Subtokens.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Subtokens
{
    public class SubtokenReaderTest : IClassFixture<SubtokenFixture>
    {
        public SubtokenReaderTest(SubtokenFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly SubtokenFixture _fixture;

        [Fact]
        [Trait("Feature", "Subtokens")]
        [Trait("Category", "Integration")]
        [Trait("Importance", "Critical")]
        public void Subtokens_can_be_created_from_json()
        {
            var sut = new SubtokenReader();

            using var document = JsonDocument.Parse(_fixture.CreatedSubtoken);

            var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

            Assert.NotEmpty(actual.Subtoken);
        }
    }
}
