using System.Text.Json;

using GuildWars2.Hero.Accounts;
using GuildWars2.Items;

namespace GuildWars2.Tests.Features;

public class ExtensibleEnum
{
    [Fact]
    public void Indicates_when_enum_name_is_defined()
    {
        var extensible = new Extensible<Rarity>(nameof(Rarity.Legendary));
        Assert.True(extensible.IsDefined());
    }

    [Fact]
    public void Indicates_when_enum_name_is_not_defined()
    {
        var extensible = new Extensible<Rarity>("Mythical");
        Assert.False(extensible.IsDefined());
    }

    [Fact]
    public void Indicates_when_enum_name_is_not_defined_when_enum_has_a_default_value()
    {
        var extensible = new Extensible<ProductName>("GuildWars3");
        Assert.False(extensible.IsDefined());
    }

    [Fact]
    public void Returns_enum_name_as_string()
    {
        var extensible = new Extensible<Rarity>(nameof(Rarity.Legendary));
        Assert.Equal(nameof(Rarity.Legendary), extensible.ToString());
    }

    [Fact]
    public void Implicitly_converts_enum_to_extensible_enum()
    {
        Extensible<Rarity> extensible = Rarity.Legendary;
        Assert.Equal(nameof(Rarity.Legendary), extensible.ToString());
    }

    [Fact]
    public void Implicitly_converts_string_to_extensible_enum()
    {
        Extensible<Rarity> extensible = nameof(Rarity.Legendary);
        Assert.Equal(nameof(Rarity.Legendary), extensible.ToString());
    }

    [Fact]
    public void Converts_default_to_default_enum_value()
    {
        Extensible<MissingMemberBehavior> extensible = default;
        Assert.True(extensible.IsDefined());
        Assert.Equal(MissingMemberBehavior.Error, extensible.ToEnum());
        Assert.Equal("Error", extensible.ToString());
    }

    [Fact]
    public void Indicates_when_value_equals_other_value()
    {
        var left = new Extensible<Rarity>(nameof(Rarity.Legendary));
        var right = new Extensible<Rarity>(nameof(Rarity.Legendary));
        Assert.True(left == right);
        Assert.True(right == left);
    }

    [Fact]
    public void Indicates_when_value_equals_enum_value()
    {
        var left = new Extensible<Rarity>(nameof(Rarity.Legendary));
        var right = Rarity.Legendary;
        Assert.True(left == right);
        Assert.True(right == left);
    }

    [Fact]
    public void Indicates_when_value_equals_string_value()
    {
        var left = new Extensible<Rarity>(nameof(Rarity.Legendary));
        var right = nameof(Rarity.Legendary);
        Assert.True(left == right);
        Assert.True(right == left);
    }

    [Fact]
    public void Does_case_insensitivate_equality_check()
    {
        var left = new Extensible<Rarity>(nameof(Rarity.Legendary).ToLowerInvariant());
        var right = new Extensible<Rarity>(nameof(Rarity.Legendary).ToUpperInvariant());
        Assert.True(left == right);
        Assert.True(right == left);
    }

    [Fact]
    public void Converts_names_to_enum()
    {
        var extensible = new Extensible<Rarity>(nameof(Rarity.Legendary));
        var actual = extensible.ToEnum();
        Assert.Equal(Rarity.Legendary, actual);
    }

    [Fact]
    public void Converts_unknown_names_to_null()
    {
        var extensible = new Extensible<Rarity>("Mythical");
        var actual = extensible.ToEnum();
        Assert.Null(actual);
    }

    [Fact]
    public void Converts_unknown_names_to_null_when_enum_has_a_default_value()
    {
        var extensible = new Extensible<ProductName>("GuildWars3");
        var actual = extensible.ToEnum();
        Assert.Null(actual);
    }

    [Fact]
    public void Has_json_conversion()
    {
        Extensible<ProductName> extensible = ProductName.GuildWars2;
        var json = JsonSerializer.Serialize(extensible);
        var actual = JsonSerializer.Deserialize<Extensible<ProductName>>(json);
        Assert.Equal(extensible, actual);
    }

    [Fact]
    public void Has_json_conversion_for_undefined_values()
    {
        Extensible<ProductName> extensible = "GuildWars3";
        var json = JsonSerializer.Serialize(extensible);
        var actual = JsonSerializer.Deserialize<Extensible<ProductName>>(json);
        Assert.Equal(extensible, actual);
    }
}
