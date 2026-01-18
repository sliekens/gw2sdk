using System.Text.Json;

using GuildWars2.Collections;
using GuildWars2.Hero;

namespace GuildWars2.ArchitectureTests;

public class ImmutableValueDictionaryTest
{
    private sealed record SampleRecord(ImmutableValueDictionary<string, int> Data);

    [Test]
    public async Task Empty_dictionary_has_zero_count()
    {
        ImmutableValueDictionary<string, int> dict = [];

        await Assert.That(dict.Count).IsEqualTo(0);
    }

    [Test]
    public async Task Dictionaries_with_same_entries_are_equal()
    {
        ImmutableValueDictionary<string, int> dict1 = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });
        ImmutableValueDictionary<string, int> dict2 = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });

        await Assert.That(dict1.Equals(dict2)).IsTrue();
        await Assert.That(dict1 == dict2).IsTrue();
        await Assert.That(dict1 != dict2).IsFalse();
    }

    [Test]
    public async Task Dictionaries_with_same_entries_in_different_order_are_equal()
    {
        ImmutableValueDictionary<string, int> dict1 = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });
        ImmutableValueDictionary<string, int> dict2 = new(new Dictionary<string, int> { ["b"] = 2, ["a"] = 1 });

        await Assert.That(dict1.Equals(dict2)).IsTrue();
        await Assert.That(dict1 == dict2).IsTrue();
    }

    [Test]
    public async Task Dictionaries_with_different_values_are_not_equal()
    {
        ImmutableValueDictionary<string, int> dict1 = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });
        ImmutableValueDictionary<string, int> dict2 = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 99 });

        await Assert.That(dict1.Equals(dict2)).IsFalse();
        await Assert.That(dict1 == dict2).IsFalse();
        await Assert.That(dict1 != dict2).IsTrue();
    }

    [Test]
    public async Task Dictionaries_with_different_keys_are_not_equal()
    {
        ImmutableValueDictionary<string, int> dict1 = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });
        ImmutableValueDictionary<string, int> dict2 = new(new Dictionary<string, int> { ["a"] = 1, ["c"] = 2 });

        await Assert.That(dict1.Equals(dict2)).IsFalse();
        await Assert.That(dict1 == dict2).IsFalse();
    }

    [Test]
    public async Task Dictionaries_with_different_counts_are_not_equal()
    {
        ImmutableValueDictionary<string, int> dict1 = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });
        ImmutableValueDictionary<string, int> dict2 = new(new Dictionary<string, int> { ["a"] = 1 });

        await Assert.That(dict1.Equals(dict2)).IsFalse();
        await Assert.That(dict1 == dict2).IsFalse();
    }

    [Test]
    public async Task Same_reference_is_equal()
    {
        ImmutableValueDictionary<string, int> dict = new(new Dictionary<string, int> { ["a"] = 1 });

        await Assert.That(dict.Equals(dict)).IsTrue();
#pragma warning disable CS1718 // Comparison made to same variable
        await Assert.That(dict == dict).IsTrue();
#pragma warning restore CS1718
    }

    [Test]
    public async Task Null_is_not_equal()
    {
        ImmutableValueDictionary<string, int> dict = new(new Dictionary<string, int> { ["a"] = 1 });

#pragma warning disable CA1508 // Intentionally testing null comparison
        await Assert.That(dict.Equals(null)).IsFalse();
        await Assert.That(dict == null).IsFalse();
        await Assert.That(null == dict).IsFalse();
#pragma warning restore CA1508
    }

    [Test]
    public async Task Both_null_are_equal()
    {
        ImmutableValueDictionary<string, int>? left = null;
        ImmutableValueDictionary<string, int>? right = null;

#pragma warning disable CA1508 // Intentionally testing null comparison
        await Assert.That(left == right).IsTrue();
#pragma warning restore CA1508
    }

    [Test]
    public async Task Equal_dictionaries_have_same_hash_code()
    {
        ImmutableValueDictionary<string, int> dict1 = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });
        ImmutableValueDictionary<string, int> dict2 = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });

        await Assert.That(dict1.GetHashCode()).IsEqualTo(dict2.GetHashCode());
    }

    [Test]
    public async Task Equal_dictionaries_with_different_order_have_same_hash_code()
    {
        ImmutableValueDictionary<string, int> dict1 = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });
        ImmutableValueDictionary<string, int> dict2 = new(new Dictionary<string, int> { ["b"] = 2, ["a"] = 1 });

        await Assert.That(dict1.GetHashCode()).IsEqualTo(dict2.GetHashCode());
    }

    [Test]
    public async Task Different_dictionaries_have_different_hash_codes()
    {
        ImmutableValueDictionary<string, int> dict1 = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });
        ImmutableValueDictionary<string, int> dict2 = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 99 });

        await Assert.That(dict1.GetHashCode()).IsNotEqualTo(dict2.GetHashCode());
    }

    [Test]
    public async Task Records_with_equal_dictionaries_are_equal()
    {
        ImmutableValueDictionary<string, int> dict1 = new(new Dictionary<string, int> { ["a"] = 1 });
        ImmutableValueDictionary<string, int> dict2 = new(new Dictionary<string, int> { ["a"] = 1 });

        SampleRecord left = new(dict1);
        SampleRecord right = new(dict2);

        await Assert.That(left.Data.Equals(right.Data)).IsTrue();
        await Assert.That(left == right).IsTrue();
    }

    [Test]
    public async Task Records_with_different_dictionaries_are_not_equal()
    {
        ImmutableValueDictionary<string, int> dict1 = new(new Dictionary<string, int> { ["a"] = 1 });
        ImmutableValueDictionary<string, int> dict2 = new(new Dictionary<string, int> { ["b"] = 2 });

        SampleRecord left = new(dict1);
        SampleRecord right = new(dict2);

        await Assert.That(left.Data.Equals(right.Data)).IsFalse();
        await Assert.That(left == right).IsFalse();
    }

    [Test]
    public async Task Add_returns_new_dictionary_with_entry()
    {
        ImmutableValueDictionary<string, int> original = new(new Dictionary<string, int> { ["a"] = 1 });
        ImmutableValueDictionary<string, int> modified = original.Add("b", 2);

        await Assert.That(original.Count).IsEqualTo(1);
        await Assert.That(modified.Count).IsEqualTo(2);
        await Assert.That(modified["b"]).IsEqualTo(2);
    }

    [Test]
    public async Task SetItem_returns_new_dictionary_with_updated_entry()
    {
        ImmutableValueDictionary<string, int> original = new(new Dictionary<string, int> { ["a"] = 1 });
        ImmutableValueDictionary<string, int> modified = original.SetItem("a", 99);

        await Assert.That(original["a"]).IsEqualTo(1);
        await Assert.That(modified["a"]).IsEqualTo(99);
    }

    [Test]
    public async Task SetItem_adds_new_key_if_not_exists()
    {
        ImmutableValueDictionary<string, int> original = new(new Dictionary<string, int> { ["a"] = 1 });
        ImmutableValueDictionary<string, int> modified = original.SetItem("b", 2);

        await Assert.That(original.Count).IsEqualTo(1);
        await Assert.That(modified.Count).IsEqualTo(2);
        await Assert.That(modified["b"]).IsEqualTo(2);
    }

    [Test]
    public async Task AddRange_returns_new_dictionary_with_entries()
    {
        ImmutableValueDictionary<string, int> original = new(new Dictionary<string, int> { ["a"] = 1 });
        ImmutableValueDictionary<string, int> modified = original.AddRange(new Dictionary<string, int> { ["b"] = 2, ["c"] = 3 });

        await Assert.That(original.Count).IsEqualTo(1);
        await Assert.That(modified.Count).IsEqualTo(3);
    }

    [Test]
    public async Task Remove_returns_new_dictionary_without_key()
    {
        ImmutableValueDictionary<string, int> original = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });
        ImmutableValueDictionary<string, int> modified = original.Remove("a");

        await Assert.That(original.Count).IsEqualTo(2);
        await Assert.That(modified.Count).IsEqualTo(1);
        await Assert.That(modified.ContainsKey("a")).IsFalse();
    }

    [Test]
    public async Task Remove_nonexistent_key_returns_same_dictionary()
    {
        ImmutableValueDictionary<string, int> original = new(new Dictionary<string, int> { ["a"] = 1 });
        ImmutableValueDictionary<string, int> modified = original.Remove("nonexistent");

        await Assert.That(ReferenceEquals(original, modified)).IsTrue();
    }

    [Test]
    public async Task RemoveRange_returns_new_dictionary_without_keys()
    {
        ImmutableValueDictionary<string, int> original = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 });
        ImmutableValueDictionary<string, int> modified = original.RemoveRange(["a", "c"]);

        await Assert.That(original.Count).IsEqualTo(3);
        await Assert.That(modified.Count).IsEqualTo(1);
        await Assert.That(modified.ContainsKey("b")).IsTrue();
    }

    [Test]
    public async Task Clear_returns_empty_dictionary()
    {
        ImmutableValueDictionary<string, int> original = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });
        ImmutableValueDictionary<string, int> cleared = original.Clear();

        await Assert.That(original.Count).IsEqualTo(2);
        await Assert.That(cleared.Count).IsEqualTo(0);
        await Assert.That(ReferenceEquals(cleared, ImmutableValueDictionary<string, int>.Empty)).IsTrue();
    }

    [Test]
    public async Task ContainsKey_returns_true_for_existing_key()
    {
        ImmutableValueDictionary<string, int> dict = new(new Dictionary<string, int> { ["a"] = 1 });

        await Assert.That(dict.ContainsKey("a")).IsTrue();
        await Assert.That(dict.ContainsKey("nonexistent")).IsFalse();
    }

    [Test]
    public async Task TryGetValue_returns_value_for_existing_key()
    {
        ImmutableValueDictionary<string, int> dict = new(new Dictionary<string, int> { ["a"] = 1 });

        bool found = dict.TryGetValue("a", out int value);

        await Assert.That(found).IsTrue();
        await Assert.That(value).IsEqualTo(1);
    }

    [Test]
    public async Task TryGetValue_returns_false_for_nonexistent_key()
    {
        ImmutableValueDictionary<string, int> dict = new(new Dictionary<string, int> { ["a"] = 1 });

        bool found = dict.TryGetValue("nonexistent", out _);

        await Assert.That(found).IsFalse();
    }

    [Test]
    public async Task Indexer_returns_value_for_key()
    {
        ImmutableValueDictionary<string, int> dict = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });

        await Assert.That(dict["a"]).IsEqualTo(1);
        await Assert.That(dict["b"]).IsEqualTo(2);
    }

    [Test]
    public async Task Keys_returns_all_keys()
    {
        ImmutableValueDictionary<string, int> dict = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });

        await Assert.That(dict.Keys).Contains("a");
        await Assert.That(dict.Keys).Contains("b");
    }

    [Test]
    public async Task Values_returns_all_values()
    {
        ImmutableValueDictionary<string, int> dict = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });

        await Assert.That(dict.Values).Contains(1);
        await Assert.That(dict.Values).Contains(2);
    }

    [Test]
    public async Task Can_enumerate_entries()
    {
        ImmutableValueDictionary<string, int> dict = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });
        List<KeyValuePair<string, int>> result = [];

        foreach (KeyValuePair<string, int> kvp in dict)
        {
            result.Add(kvp);
        }

        await Assert.That(result).Count().IsEqualTo(2);
    }

    [Test]
    public async Task Interface_Add_returns_IImmutableValueDictionary()
    {
        IImmutableValueDictionary<string, int> dict = new ImmutableValueDictionary<string, int>(new Dictionary<string, int> { ["a"] = 1 });

        IImmutableValueDictionary<string, int> result = dict.Add("b", 2);

        await Assert.That(result.Count).IsEqualTo(2);
        await Assert.That(result["b"]).IsEqualTo(2);
    }

    [Test]
    public async Task Interface_AddRange_returns_IImmutableValueDictionary()
    {
        IImmutableValueDictionary<string, int> dict = new ImmutableValueDictionary<string, int>(new Dictionary<string, int> { ["a"] = 1 });

        IImmutableValueDictionary<string, int> result = dict.AddRange(new Dictionary<string, int> { ["b"] = 2, ["c"] = 3 });

        await Assert.That(result.Count).IsEqualTo(3);
    }

    [Test]
    public async Task Interface_Clear_returns_IImmutableValueDictionary()
    {
        IImmutableValueDictionary<string, int> dict = new ImmutableValueDictionary<string, int>(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });

        IImmutableValueDictionary<string, int> result = dict.Clear();

        await Assert.That(result.Count).IsEqualTo(0);
    }

    [Test]
    public async Task Interface_Remove_returns_IImmutableValueDictionary()
    {
        IImmutableValueDictionary<string, int> dict = new ImmutableValueDictionary<string, int>(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });

        IImmutableValueDictionary<string, int> result = dict.Remove("a");

        await Assert.That(result.Count).IsEqualTo(1);
        await Assert.That(result.ContainsKey("a")).IsFalse();
    }

    [Test]
    public async Task Interface_RemoveRange_returns_IImmutableValueDictionary()
    {
        IImmutableValueDictionary<string, int> dict = new ImmutableValueDictionary<string, int>(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 });

        IImmutableValueDictionary<string, int> result = dict.RemoveRange(["a", "b"]);

        await Assert.That(result.Count).IsEqualTo(1);
        await Assert.That(result.ContainsKey("c")).IsTrue();
    }

    [Test]
    public async Task Interface_SetItem_returns_IImmutableValueDictionary()
    {
        IImmutableValueDictionary<string, int> dict = new ImmutableValueDictionary<string, int>(new Dictionary<string, int> { ["a"] = 1 });

        IImmutableValueDictionary<string, int> result = dict.SetItem("a", 99);

        await Assert.That(result["a"]).IsEqualTo(99);
    }

    [Test]
    public async Task Interface_SetItems_returns_IImmutableValueDictionary()
    {
        IImmutableValueDictionary<string, int> dict = new ImmutableValueDictionary<string, int>(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });

        IImmutableValueDictionary<string, int> result = dict.SetItems(new Dictionary<string, int> { ["a"] = 10, ["b"] = 20 });

        await Assert.That(result["a"]).IsEqualTo(10);
        await Assert.That(result["b"]).IsEqualTo(20);
    }

    [Test]
    public async Task Json_roundtrip_serialization_preserves_elements()
    {
        ImmutableValueDictionary<string, int> original = new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });

        string json = JsonSerializer.Serialize(original);
        ImmutableValueDictionary<string, int>? deserialized = JsonSerializer.Deserialize<ImmutableValueDictionary<string, int>>(json);

        await Assert.That(deserialized).IsEqualTo(original);
    }

    [Test]
    public async Task Interface_json_roundtrip_serialization_preserves_elements()
    {
        IImmutableValueDictionary<string, int> original = new ImmutableValueDictionary<string, int>(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });

        string json = JsonSerializer.Serialize(original);
        IImmutableValueDictionary<string, int>? deserialized = JsonSerializer.Deserialize<IImmutableValueDictionary<string, int>>(json);

        await Assert.That(deserialized).IsNotNull();
        await Assert.That(deserialized!["a"]).IsEqualTo(1);
        await Assert.That(deserialized["b"]).IsEqualTo(2);
    }

    [Test]
    public async Task Can_deserialize_json_object_with_extensible_enum_keys()
    {
        const string json = """{"Toughness":239,"HealingPower":171,"Concentration":171}""";

        IImmutableValueDictionary<Extensible<AttributeName>, int>? result =
            JsonSerializer.Deserialize<IImmutableValueDictionary<Extensible<AttributeName>, int>>(json);

        await Assert.That(result).IsNotNull();
        await Assert.That(result![AttributeName.Toughness]).IsEqualTo(239);
        await Assert.That(result[AttributeName.HealingPower]).IsEqualTo(171);
        await Assert.That(result[AttributeName.Concentration]).IsEqualTo(171);
    }

    [Test]
    public async Task Can_serialize_dictionary_with_extensible_enum_keys()
    {
        IImmutableValueDictionary<Extensible<AttributeName>, int> sut =
            new ImmutableValueDictionary<Extensible<AttributeName>, int>(
                new Dictionary<Extensible<AttributeName>, int>
                {
                    [AttributeName.Toughness] = 239,
                    [AttributeName.HealingPower] = 171,
                    [AttributeName.Concentration] = 171,
                }
            );

        string json = JsonSerializer.Serialize(sut);
        ImmutableValueDictionary<Extensible<AttributeName>, int>? deserialized =
            JsonSerializer.Deserialize<ImmutableValueDictionary<Extensible<AttributeName>, int>>(json);

        await Assert.That(deserialized).IsEqualTo(sut);
    }
}
