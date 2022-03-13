using System.Text.Json;
using GW2SDK.Items;
using GW2SDK.Json;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    public class ItemReaderTest : IClassFixture<ItemFixture>
    {
        public ItemReaderTest(ItemFixture fixture)
        {
            this.fixture = fixture;
        }

        private readonly ItemFixture fixture;

        [Fact]
        public void Items_can_be_created_from_json()
        {
            var sut = new ItemReader();

            Assert.All(fixture.Items,
                json =>
                {
                    using var document = JsonDocument.Parse(json);
                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);
                    ItemFacts.Validate(actual);
                });
        }
    }
}
