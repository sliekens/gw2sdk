using System.Text.Json;
using GW2SDK.Items;
using GW2SDK.Json;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    public class ItemReaderTest : IClassFixture<ItemFixture>
    {
        public ItemReaderTest(ItemFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ItemFixture _fixture;

        [Fact]
        [Trait("Feature", "Items")]
        [Trait("Category", "Integration")]
        [Trait("Importance", "Critical")]
        public void Items_can_be_created_from_json()
        {
            var sut = new ItemReader();

            AssertEx.ForEach(_fixture.Items,
                json =>
                {
                    using var document = JsonDocument.Parse(json);
                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);
                    ItemFacts.Validate(actual);
                });
        }
    }
}
