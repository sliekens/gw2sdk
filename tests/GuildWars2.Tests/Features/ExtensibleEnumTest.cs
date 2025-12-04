using System.Text.Json;

using GuildWars2.Hero.Accounts;
using GuildWars2.Items;

namespace GuildWars2.Tests.Features;

public class ExtensibleEnumTest
{
    [Test]
    public async Task Indicates_when_enum_name_is_defined()
    {
        Extensible<Rarity> extensible = new(nameof(Rarity.Legendary));
        await Assert.That(extensible.IsDefined()).IsTrue();
    }

    [Test]
    public async Task Indicates_when_enum_name_is_not_defined()
    {
        Extensible<Rarity> extensible = new("Mythical");
        await Assert.That(extensible.IsDefined()).IsFalse();
    }

    [Test]
    public async Task Indicates_when_enum_name_is_not_defined_when_enum_has_a_default_value()
    {
        Extensible<ProductName> extensible = new("GuildWars3");
        await Assert.That(extensible.IsDefined()).IsFalse();
    }

    [Test]
    public async Task Returns_enum_name_as_string()
    {
        Extensible<Rarity> extensible = new(nameof(Rarity.Legendary));
        await Assert.That(extensible.ToString()).IsEqualTo(nameof(Rarity.Legendary));
    }

    [Test]
    public async Task Implicitly_converts_enum_to_extensible_enum()
    {
        Extensible<Rarity> extensible = Rarity.Legendary;
        await Assert.That(extensible.ToString()).IsEqualTo(nameof(Rarity.Legendary));
    }

    [Test]
    public async Task Implicitly_converts_string_to_extensible_enum()
    {
        Extensible<Rarity> extensible = nameof(Rarity.Legendary);
        await Assert.That(extensible.ToString()).IsEqualTo(nameof(Rarity.Legendary));
    }

    [Test]
    public async Task Converts_default_to_default_enum_value()
    {
        Extensible<MissingMemberBehavior> extensible = default;
        await Assert.That(extensible).Member(e => e.IsDefined(), isDefined => isDefined.IsTrue())
            .And.Member(e => e.ToEnum(), toEnum => toEnum.IsEqualTo(MissingMemberBehavior.Error))
            .And.Member(e => e.ToString(), toString => toString.IsEqualTo("Error"));
    }

    [Test]
    public async Task Indicates_when_value_equals_other_value()
    {
        Extensible<Rarity> left = new(nameof(Rarity.Legendary));
        Extensible<Rarity> right = new(nameof(Rarity.Legendary));
        await Assert.That(left == right).IsTrue();
        await Assert.That(right == left).IsTrue();
    }

    [Test]
    public async Task Indicates_when_value_equals_enum_value()
    {
        Extensible<Rarity> left = new(nameof(Rarity.Legendary));
        Rarity right = Rarity.Legendary;
        await Assert.That(left == right).IsTrue();
        await Assert.That(right == left).IsTrue();
    }

    [Test]
    public async Task Indicates_when_value_equals_string_value()
    {
        Extensible<Rarity> left = new(nameof(Rarity.Legendary));
        string right = nameof(Rarity.Legendary);
        await Assert.That(left == right).IsTrue();
        await Assert.That(right == left).IsTrue();
    }

    [Test]
    public async Task Does_case_insensitivate_equality_check()
    {
        Extensible<Rarity> left = new("legendary");
        Extensible<Rarity> right = new("LEGENDARY");
        await Assert.That(left == right).IsTrue();
        await Assert.That(right == left).IsTrue();
    }

    [Test]
    public async Task Converts_names_to_enum()
    {
        Extensible<Rarity> extensible = new(nameof(Rarity.Legendary));
        Rarity? actual = extensible.ToEnum();
        await Assert.That(actual).IsEqualTo(Rarity.Legendary);
    }

    [Test]
    public async Task Converts_unknown_names_to_null()
    {
        Extensible<Rarity> extensible = new("Mythical");
        Rarity? actual = extensible.ToEnum();
        await Assert.That(actual).IsNull();
    }

    [Test]
    public async Task Converts_unknown_names_to_null_when_enum_has_a_default_value()
    {
        Extensible<ProductName> extensible = new("GuildWars3");
        ProductName? actual = extensible.ToEnum();
        await Assert.That(actual).IsNull();
    }

    [Test]
    public async Task Has_json_conversion()
    {
        Extensible<ProductName> extensible = ProductName.GuildWars2;
#if NET
        string json = JsonSerializer.Serialize(extensible, Common.TestJsonContext.Default.ExtensibleProductName);
        Extensible<ProductName> actual = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.ExtensibleProductName);
#else
        string json = JsonSerializer.Serialize(extensible);
        Extensible<ProductName> actual = JsonSerializer.Deserialize<Extensible<ProductName>>(json);
#endif
        await Assert.That(actual).IsEqualTo(extensible);
    }

    [Test]
    public async Task Has_json_conversion_for_undefined_values()
    {
        Extensible<ProductName> extensible = "GuildWars3";
#if NET
        string json = JsonSerializer.Serialize(extensible, Common.TestJsonContext.Default.ExtensibleProductName);
        Extensible<ProductName> actual = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.ExtensibleProductName);
#else
        string json = JsonSerializer.Serialize(extensible);
        Extensible<ProductName> actual = JsonSerializer.Deserialize<Extensible<ProductName>>(json);
#endif
        await Assert.That(actual).IsEqualTo(extensible);
    }
}
