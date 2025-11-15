using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Hero.Accounts;
using GuildWars2.Items;

namespace GuildWars2.Tests.Features;

[JsonSerializable(typeof(Extensible<ProductName>))]
[JsonSerializable(typeof(ProductName))]
public partial class TestJsonContext : JsonSerializerContext
{
}

public class ExtensibleEnumTest
{
    [Test]
    public void Indicates_when_enum_name_is_defined()
    {
        Extensible<Rarity> extensible = new(nameof(Rarity.Legendary));
        Assert.True(extensible.IsDefined());
    }

    [Test]
    public void Indicates_when_enum_name_is_not_defined()
    {
        Extensible<Rarity> extensible = new("Mythical");
        Assert.False(extensible.IsDefined());
    }

    [Test]
    public void Indicates_when_enum_name_is_not_defined_when_enum_has_a_default_value()
    {
        Extensible<ProductName> extensible = new("GuildWars3");
        Assert.False(extensible.IsDefined());
    }

    [Test]
    public void Returns_enum_name_as_string()
    {
        Extensible<Rarity> extensible = new(nameof(Rarity.Legendary));
        Assert.Equal(nameof(Rarity.Legendary), extensible.ToString());
    }

    [Test]
    public void Implicitly_converts_enum_to_extensible_enum()
    {
        Extensible<Rarity> extensible = Rarity.Legendary;
        Assert.Equal(nameof(Rarity.Legendary), extensible.ToString());
    }

    [Test]
    public void Implicitly_converts_string_to_extensible_enum()
    {
        Extensible<Rarity> extensible = nameof(Rarity.Legendary);
        Assert.Equal(nameof(Rarity.Legendary), extensible.ToString());
    }

    [Test]
    public void Converts_default_to_default_enum_value()
    {
        Extensible<MissingMemberBehavior> extensible = default;
        Assert.True(extensible.IsDefined());
        Assert.Equal(MissingMemberBehavior.Error, extensible.ToEnum());
        Assert.Equal("Error", extensible.ToString());
    }

    [Test]
    public void Indicates_when_value_equals_other_value()
    {
        Extensible<Rarity> left = new(nameof(Rarity.Legendary));
        Extensible<Rarity> right = new(nameof(Rarity.Legendary));
        Assert.True(left == right);
        Assert.True(right == left);
    }

    [Test]
    public void Indicates_when_value_equals_enum_value()
    {
        Extensible<Rarity> left = new(nameof(Rarity.Legendary));
        Rarity right = Rarity.Legendary;
        Assert.True(left == right);
        Assert.True(right == left);
    }

    [Test]
    public void Indicates_when_value_equals_string_value()
    {
        Extensible<Rarity> left = new(nameof(Rarity.Legendary));
        string right = nameof(Rarity.Legendary);
        Assert.True(left == right);
        Assert.True(right == left);
    }

    [Test]
    public void Does_case_insensitivate_equality_check()
    {
        Extensible<Rarity> left = new("legendary");
        Extensible<Rarity> right = new("LEGENDARY");
        Assert.True(left == right);
        Assert.True(right == left);
    }

    [Test]
    public void Converts_names_to_enum()
    {
        Extensible<Rarity> extensible = new(nameof(Rarity.Legendary));
        Rarity? actual = extensible.ToEnum();
        Assert.Equal(Rarity.Legendary, actual);
    }

    [Test]
    public void Converts_unknown_names_to_null()
    {
        Extensible<Rarity> extensible = new("Mythical");
        Rarity? actual = extensible.ToEnum();
        Assert.Null(actual);
    }

    [Test]
    public void Converts_unknown_names_to_null_when_enum_has_a_default_value()
    {
        Extensible<ProductName> extensible = new("GuildWars3");
        ProductName? actual = extensible.ToEnum();
        Assert.Null(actual);
    }

    [Test]
    public void Has_json_conversion()
    {
        Extensible<ProductName> extensible = ProductName.GuildWars2;
#if NET
        string json = JsonSerializer.Serialize(extensible, TestJsonContext.Default.ExtensibleProductName);
        Extensible<ProductName> actual = JsonSerializer.Deserialize(json, TestJsonContext.Default.ExtensibleProductName);
#else
        string json = JsonSerializer.Serialize(extensible);
        Extensible<ProductName> actual = JsonSerializer.Deserialize<Extensible<ProductName>>(json);
#endif
        Assert.Equal(extensible, actual);
    }

    [Test]
    public void Has_json_conversion_for_undefined_values()
    {
        Extensible<ProductName> extensible = "GuildWars3";
#if NET
        string json = JsonSerializer.Serialize(extensible, TestJsonContext.Default.ExtensibleProductName);
        Extensible<ProductName> actual = JsonSerializer.Deserialize(json, TestJsonContext.Default.ExtensibleProductName);
#else
        string json = JsonSerializer.Serialize(extensible);
        Extensible<ProductName> actual = JsonSerializer.Deserialize<Extensible<ProductName>>(json);
#endif
        Assert.Equal(extensible, actual);
    }
}
