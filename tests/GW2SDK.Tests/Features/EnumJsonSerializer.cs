using System.Text.Json;

using GuildWars2.Hero.Accounts;


namespace GuildWars2.Tests.Features;

public class EnumJsonSerializer
{
    [Test]
    public async Task Has_json_conversion()
    {
        ProductName product = ProductName.GuildWars2;
#if NET
        string json = JsonSerializer.Serialize(product, Common.TestJsonContext.Default.ProductName);
        ProductName actual = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.ProductName);
#else
        string json = JsonSerializer.Serialize(product);
        ProductName actual = JsonSerializer.Deserialize<ProductName>(json);
#endif
        await Assert.That(actual).IsEqualTo(product);
    }

    [Test]
    public async Task Throws_for_undefined_values()
    {
        ArgumentOutOfRangeException? ex = await Assert.That(() =>

        {
            ProductName product = (ProductName)69;
#if NET
            _ = JsonSerializer.Serialize(product, Common.TestJsonContext.Default.ProductName);
#else
            _ = JsonSerializer.Serialize(product);
#endif
        }).Throws<ArgumentOutOfRangeException>();
    }
}
