using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

using GuildWars2.Collections;
using GuildWars2.Hero;

namespace GuildWars2.ArchitectureTests;

/// <summary>Round-trips the immutable value collections through a source-generated <see cref="JsonSerializerContext" />
/// (whose options have no reflection fallback) and through the reflection-based serializer, and checks that both produce
/// the same JSON as the equivalent BCL collections. See https://github.com/sliekens/gw2sdk/issues/521.</summary>
[Feature("Architecture")]
public class ImmutableValueCollectionJsonTest
{
    private static readonly JsonSerializerOptions ReflectionOptions = new()
    {
        TypeInfoResolver = new DefaultJsonTypeInfoResolver()
    };

    private static async Task AssertRoundtrip<T>(T original, JsonTypeInfo<T> typeInfo, string expectedJson)
    {
        // Source-generated: the context's options resolve metadata from the context only
        string generatedJson = JsonSerializer.Serialize(original, typeInfo);
        T? generated = JsonSerializer.Deserialize(generatedJson, typeInfo);

        // Reflection
        string reflectionJson = JsonSerializer.Serialize(original, ReflectionOptions);
        T? reflection = JsonSerializer.Deserialize<T>(reflectionJson, ReflectionOptions);

        await Assert.That(generatedJson).IsEqualTo(expectedJson);
        await Assert.That(reflectionJson).IsEqualTo(expectedJson);
        await Assert.That(generated).IsEqualTo(original);
        await Assert.That(reflection).IsEqualTo(original);
    }

    private static string Bcl<T>(T value)
    {
        return JsonSerializer.Serialize(value, ReflectionOptions);
    }

    [Test]
    public async Task Interface_list_of_int_roundtrips()
    {
        IImmutableValueList<int> original = new ImmutableValueList<int>([1, 2, 3]);

        await AssertRoundtrip(original, CollectionsJsonContext.Default.IImmutableValueListInt32, Bcl<List<int>>([1, 2, 3]));
    }

    [Test]
    public async Task List_of_string_roundtrips()
    {
        ImmutableValueList<string?> original = ["a", null, "c"];

        await AssertRoundtrip(original, CollectionsJsonContext.Default.ImmutableValueListString, Bcl<List<string?>>(["a", null, "c"]));
    }

    [Test]
    public async Task List_of_nullable_int_roundtrips()
    {
        ImmutableValueList<int?> original = [1, null, 3];

        await AssertRoundtrip(original, CollectionsJsonContext.Default.ImmutableValueListNullableInt32, "[1,null,3]");
    }

    [Test]
    public async Task Array_of_int_roundtrips()
    {
        ImmutableValueArray<int> original = [1, 2, 3];

        await AssertRoundtrip(original, CollectionsJsonContext.Default.ImmutableValueArrayInt32, Bcl<int[]>([1, 2, 3]));
    }

    [Test]
    public async Task Array_of_string_roundtrips()
    {
        ImmutableValueArray<string> original = ["a", "b"];

        await AssertRoundtrip(original, CollectionsJsonContext.Default.ImmutableValueArrayString, Bcl<string[]>(["a", "b"]));
    }

    [Test]
    public async Task Interface_set_of_int_roundtrips()
    {
        IImmutableValueSet<int> original = new ImmutableValueSet<int>([42]);

        await AssertRoundtrip(original, CollectionsJsonContext.Default.IImmutableValueSetInt32, "[42]");
    }

    [Test]
    public async Task Set_of_string_roundtrips()
    {
        ImmutableValueSet<string> original = ["a"];

        await AssertRoundtrip(original, CollectionsJsonContext.Default.ImmutableValueSetString, """["a"]""");
    }

    [Test]
    public async Task Interface_dictionary_of_int_keys_roundtrips()
    {
        IImmutableValueDictionary<int, int> original = new ImmutableValueDictionary<int, int>(
            new Dictionary<int, int> { [1] = 10 }
        );

        await AssertRoundtrip(
            original,
            CollectionsJsonContext.Default.IImmutableValueDictionaryInt32Int32,
            Bcl(new Dictionary<int, int> { [1] = 10 })
        );
    }

    [Test]
    public async Task Dictionary_of_string_keys_roundtrips()
    {
        ImmutableValueDictionary<string, string?> original = new(
            new Dictionary<string, string?> { ["a"] = null }
        );

        await AssertRoundtrip(
            original,
            CollectionsJsonContext.Default.ImmutableValueDictionaryStringString,
            Bcl(new Dictionary<string, string?> { ["a"] = null })
        );
    }

    [Test]
    public async Task Collections_of_sdk_types_roundtrip_as_members()
    {
        CollectionsHolder original = new(
            Prices: [new Coin(1, 2, 3), Coin.Zero],
            PriceArray: [new Coin(5)],
            PriceSet: new ImmutableValueSet<Coin>([new Coin(7)]),
            Attributes: new ImmutableValueDictionary<Extensible<AttributeName>, Coin>(
                new Dictionary<Extensible<AttributeName>, Coin> { [AttributeName.Power] = new Coin(9) }
            ),
            AttributeNames: [AttributeName.Power, "Unknown"],
            Nested: [[1, 2], []]
        );

        string expected = Bcl(
            new
            {
                Prices = new List<Coin> { new(1, 2, 3), Coin.Zero },
                PriceArray = new[] { new Coin(5) },
                PriceSet = new[] { new Coin(7) },
                Attributes = new Dictionary<Extensible<AttributeName>, Coin> { [AttributeName.Power] = new Coin(9) },
                AttributeNames = new Extensible<AttributeName>[] { AttributeName.Power, "Unknown" },
                Nested = new int[][] { [1, 2], [] }
            }
        );

        await AssertRoundtrip(original, CollectionsJsonContext.Default.CollectionsHolder, expected);
    }

    [Test]
    public async Task Null_collections_roundtrip()
    {
        NullableHolder original = new(null, null, null, null);

        await AssertRoundtrip(
            original,
            CollectionsJsonContext.Default.NullableHolder,
            """{"List":null,"Array":null,"Set":null,"Dictionary":null}"""
        );
    }

    [Test]
    public async Task Null_element_in_value_type_list_is_rejected()
    {
        await Assert.That(() => JsonSerializer.Deserialize("[1,null]", CollectionsJsonContext.Default.IImmutableValueListInt32))
            .Throws<JsonException>();
        await Assert.That(() => JsonSerializer.Deserialize<IImmutableValueList<int>>("[1,null]", ReflectionOptions))
            .Throws<JsonException>();
    }

    [Test]
    public async Task Invalid_token_is_rejected()
    {
        await Assert.That(() => JsonSerializer.Deserialize("{}", CollectionsJsonContext.Default.IImmutableValueListInt32))
            .Throws<JsonException>();
        await Assert.That(() => JsonSerializer.Deserialize("[]", CollectionsJsonContext.Default.IImmutableValueDictionaryInt32Int32))
            .Throws<JsonException>();
    }

    [Test]
    public async Task Missing_element_metadata_names_the_element_type()
    {
        IImmutableValueList<long> original = new ImmutableValueList<long>([1L]);

        NotSupportedException? exception = await Assert.That(
                () => JsonSerializer.Serialize(original, MissingElementJsonContext.Default.IImmutableValueListInt64)
            )
            .Throws<NotSupportedException>();

        await Assert.That(exception!.Message).Contains("System.Int64");
        await Assert.That(exception.Message).DoesNotContain("IImmutableList");
    }

    [Test]
    public async Task Sdk_element_type_with_internal_converter_needs_no_metadata()
    {
        IImmutableValueList<Coin> original = new ImmutableValueList<Coin>([new Coin(1, 2, 3)]);

        string json = JsonSerializer.Serialize(original, MissingElementJsonContext.Default.IImmutableValueListCoin);
        IImmutableValueList<Coin>? actual = JsonSerializer.Deserialize(json, MissingElementJsonContext.Default.IImmutableValueListCoin);

        await Assert.That(json).IsEqualTo("[10203]");
        await Assert.That(actual).IsEquivalentTo(original);
    }

    [Test]
    public async Task Dictionary_key_policy_is_applied_like_the_bcl_dictionary()
    {
        JsonSerializerOptions options = new(ReflectionOptions) { DictionaryKeyPolicy = JsonNamingPolicy.CamelCase };
        ImmutableValueDictionary<string, int> original = new(new Dictionary<string, int> { ["FooBar"] = 1 });

        string actual = JsonSerializer.Serialize(original, options);
        string expected = JsonSerializer.Serialize(new Dictionary<string, int> { ["FooBar"] = 1 }, options);

        await Assert.That(actual).IsEqualTo(expected);
    }
}

public sealed record CollectionsHolder(
    IImmutableValueList<Coin> Prices,
    ImmutableValueArray<Coin> PriceArray,
    IImmutableValueSet<Coin> PriceSet,
    IImmutableValueDictionary<Extensible<AttributeName>, Coin> Attributes,
    IImmutableValueList<Extensible<AttributeName>> AttributeNames,
    IImmutableValueList<IImmutableValueList<int>> Nested
);

public sealed record NullableHolder(
    IImmutableValueList<int>? List,
    ImmutableValueArray<string>? Array,
    IImmutableValueSet<int>? Set,
    IImmutableValueDictionary<int, int>? Dictionary
);

// The context's options have no reflection fallback, like a consumer with JsonSerializerIsReflectionEnabledByDefault=false.
// The source generator does not walk into types that have a custom [JsonConverter], so element (and key) types are
// declared explicitly, as System.Text.Json requires for any custom converter that delegates to other types.
// Coin and Extensible<T> cannot be declared (their converters are internal, SYSLIB1220); the collection converters
// fall back to the element type's [JsonConverter] for those. IImmutableList<T> and friends are no longer needed.
[JsonSerializable(typeof(int))]
[JsonSerializable(typeof(int?))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(IImmutableValueList<int>))]
[JsonSerializable(typeof(ImmutableValueList<string>))]
[JsonSerializable(typeof(ImmutableValueList<int?>))]
[JsonSerializable(typeof(ImmutableValueArray<int>))]
[JsonSerializable(typeof(ImmutableValueArray<string>))]
[JsonSerializable(typeof(IImmutableValueSet<int>))]
[JsonSerializable(typeof(ImmutableValueSet<string>))]
[JsonSerializable(typeof(IImmutableValueDictionary<int, int>))]
[JsonSerializable(typeof(ImmutableValueDictionary<string, string>))]
[JsonSerializable(typeof(CollectionsHolder))]
[JsonSerializable(typeof(NullableHolder))]
internal sealed partial class CollectionsJsonContext : JsonSerializerContext;

// Declares the collections but not their element types.
[JsonSerializable(typeof(IImmutableValueList<long>))]
[JsonSerializable(typeof(IImmutableValueList<Coin>))]
internal sealed partial class MissingElementJsonContext : JsonSerializerContext;
