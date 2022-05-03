using System.Text.Json;
using GW2SDK.Items;
using GW2SDK.Json;
using Xunit;

namespace GW2SDK.Tests.Features.Items;

public class ItemReaderTest : IClassFixture<ItemFixture>
{
    public ItemReaderTest(ItemFixture fixture)
    {
        this.fixture = fixture;
    }

    private readonly ItemFixture fixture;

    [Fact]
    public void Items_can_be_created_from_json() =>
        Assert.All(
            fixture.Items,
            json =>
            {
                using var document = JsonDocument.Parse(json);
                var actual = document.RootElement.GetItem(MissingMemberBehavior.Error);
                ItemFacts.Validate(actual);
            }
        );
}
