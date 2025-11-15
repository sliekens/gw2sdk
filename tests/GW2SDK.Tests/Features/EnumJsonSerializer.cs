using System.Text.Json;

using GuildWars2.Hero.Accounts;


namespace GuildWars2.Tests.Features;

public class EnumJsonSerializer
{
    [Test]
    public void Has_json_conversion()
    {
        ProductName product = ProductName.GuildWars2;
#if NET
        string json = JsonSerializer.Serialize(product, GW2SDK.Tests.Common.TestJsonContext.Default.ProductName);
        ProductName actual = JsonSerializer.Deserialize(json, GW2SDK.Tests.Common.TestJsonContext.Default.ProductName);
#else
        string json = JsonSerializer.Serialize(product);
        ProductName actual = JsonSerializer.Deserialize<ProductName>(json);
#endif
        Assert.Equal(product, actual);
    }

    [Test]
    public void Throws_for_undefined_values()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            ProductName product = (ProductName)69;
#if NET
            _ = JsonSerializer.Serialize(product, GW2SDK.Tests.Common.TestJsonContext.Default.ProductName);
#else
            _ = JsonSerializer.Serialize(product);
#endif
        });
    }
}
