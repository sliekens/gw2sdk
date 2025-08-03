using System.Text.Json;

using GuildWars2.Hero.Accounts;

namespace GuildWars2.Tests.Features;

public class EnumJsonSerializer
{
    [Fact]
    public void Has_json_conversion()
    {
        ProductName product = ProductName.GuildWars2;
        string json = JsonSerializer.Serialize(product);
        ProductName actual = JsonSerializer.Deserialize<ProductName>(json);
        Assert.Equal(product, actual);
    }

    [Fact]
    public void Throws_for_undefined_values()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                ProductName product = (ProductName)69;
                _ = JsonSerializer.Serialize(product);
            }
        );
    }
}
