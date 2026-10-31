using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

using GuildWars2;
using GuildWars2.AotTests;
using GuildWars2.Collections;
using GuildWars2.Hero;
using GuildWars2.Hero.Equipment;

// Round-trips the immutable value collections through a source-generated context, with reflection disabled. Under Native
// AOT this exercises the closed converter instantiations (https://github.com/sliekens/gw2sdk/issues/521). Exit code is
// the number of failed checks.
// --native: running the Native AOT binary (dotnet run sets IsDynamicCodeSupported=false too, because of PublishAot)
bool native = args.Contains("--native");
Console.WriteLine($"Native AOT: {native}, dynamic code supported: {RuntimeFeature.IsDynamicCodeSupported}");

Checks checks = new();
AotJsonContext context = AotJsonContext.Default;

// Value-type elements
checks.Roundtrip("IImmutableValueList<int>", new ImmutableValueList<int>([1, 2, 3]), () => context.IImmutableValueListInt32, "[1,2,3]");
checks.Roundtrip("ImmutableValueList<int>", [1, 2, 3], () => context.ImmutableValueListInt32, "[1,2,3]");
checks.Roundtrip("ImmutableValueArray<int>", [1, 2, 3], () => context.ImmutableValueArrayInt32, "[1,2,3]");
checks.Roundtrip("IImmutableValueSet<int>", new ImmutableValueSet<int>([1]), () => context.IImmutableValueSetInt32, "[1]");
checks.Roundtrip("ImmutableValueSet<int>", [1], () => context.ImmutableValueSetInt32, "[1]");
checks.Roundtrip("IImmutableValueList<int?>", new ImmutableValueList<int?>([1, null]), () => context.IImmutableValueListNullableInt32, "[1,null]");
checks.Roundtrip("IImmutableValueList<long>", new ImmutableValueList<long>([long.MaxValue]), () => context.IImmutableValueListInt64, "[9223372036854775807]");
checks.Roundtrip("IImmutableValueList<double>", new ImmutableValueList<double>([1.5]), () => context.IImmutableValueListDouble, "[1.5]");
checks.Roundtrip("IImmutableValueList<decimal>", new ImmutableValueList<decimal>([1.10m]), () => context.IImmutableValueListDecimal, "[1.10]");
checks.Roundtrip("IImmutableValueList<bool>", new ImmutableValueList<bool>([true]), () => context.IImmutableValueListBoolean, "[true]");
checks.Roundtrip(
    "IImmutableValueList<Guid>",
    new ImmutableValueList<Guid>([Guid.Empty]),
    () => context.IImmutableValueListGuid,
    "[\"00000000-0000-0000-0000-000000000000\"]"
);
checks.Roundtrip(
    "IImmutableValueList<DateTimeOffset>",
    new ImmutableValueList<DateTimeOffset>([DateTimeOffset.UnixEpoch]),
    () => context.IImmutableValueListDateTimeOffset,
    "[\"1970-01-01T00:00:00+00:00\"]"
);

// SDK value types, whose converters are internal (a consumer cannot declare them in a context)
checks.Roundtrip("IImmutableValueList<Coin>", new ImmutableValueList<Coin>([new Coin(1, 2, 3)]), () => context.IImmutableValueListCoin, "[10203]");
checks.Roundtrip("IImmutableValueSet<Coin>", new ImmutableValueSet<Coin>([Coin.Zero]), () => context.IImmutableValueSetCoin, "[0]");
checks.Roundtrip(
    "IImmutableValueList<Extensible<RaceName>>",
    new ImmutableValueList<Extensible<RaceName>>([RaceName.Asura, "Unknown"]),
    () => context.IImmutableValueListExtensibleRaceName,
    """["Asura","Unknown"]"""
);
checks.Roundtrip(
    "IImmutableValueList<Extensible<ProfessionName>>",
    new ImmutableValueList<Extensible<ProfessionName>>([ProfessionName.Guardian]),
    () => context.IImmutableValueListExtensibleProfessionName,
    """["Guardian"]"""
);

// Dictionaries with value-type keys and/or values
checks.Roundtrip(
    "IImmutableValueDictionary<int, int>",
    new ImmutableValueDictionary<int, int>([KeyValuePair.Create(1, 2)]),
    () => context.IImmutableValueDictionaryInt32Int32,
    """{"1":2}"""
);
checks.Roundtrip(
    "ImmutableValueDictionary<string, int>",
    new ImmutableValueDictionary<string, int>([KeyValuePair.Create("a", 1)]),
    () => context.ImmutableValueDictionaryStringInt32,
    """{"a":1}"""
);
checks.Roundtrip(
    "IImmutableValueDictionary<int, string>",
    new ImmutableValueDictionary<int, string>([KeyValuePair.Create(1, "a")]),
    () => context.IImmutableValueDictionaryInt32String,
    """{"1":"a"}"""
);
checks.Roundtrip(
    "IImmutableValueDictionary<Extensible<AttributeName>, int>",
    new ImmutableValueDictionary<Extensible<AttributeName>, int>([KeyValuePair.Create((Extensible<AttributeName>)AttributeName.Power, 1)]),
    () => context.IImmutableValueDictionaryExtensibleAttributeNameInt32,
    """{"Power":1}"""
);
checks.Roundtrip(
    "IImmutableValueDictionary<string, Coin>",
    new ImmutableValueDictionary<string, Coin>([KeyValuePair.Create("a", new Coin(5))]),
    () => context.IImmutableValueDictionaryStringCoin,
    """{"a":5}"""
);

// Reference-type elements
checks.Roundtrip("IImmutableValueList<string>", new ImmutableValueList<string?>(["a", null]), () => context.IImmutableValueListString, """["a",null]""");
checks.Roundtrip(
    "IImmutableValueList<DyeSlot>",
    new ImmutableValueList<DyeSlot?>([new DyeSlot { ColorId = 1, Material = Material.Cloth }, null]),
    () => context.IImmutableValueListDyeSlot,
    """[{"color_id":1,"material":"Cloth"},null]"""
);
checks.Roundtrip("IImmutableValueList<Item>", new ImmutableValueList<Item>([new Item("x")]), () => context.IImmutableValueListItem, """[{"Name":"x"}]""");
checks.Roundtrip(
    "IImmutableValueList<IImmutableValueList<int>>",
    new ImmutableValueList<IImmutableValueList<int>>([new ImmutableValueList<int>([1])]),
    () => context.IImmutableValueListIImmutableValueListInt32,
    "[[1]]"
);
checks.Roundtrip(
    "IImmutableValueDictionary<string, Item>",
    new ImmutableValueDictionary<string, Item>([KeyValuePair.Create("k", new Item("y"))]),
    () => context.IImmutableValueDictionaryStringItem,
    """{"k":{"Name":"y"}}"""
);

// Collections as members (the issue's repro, plus a dictionary shape that SDK models use)
checks.Roundtrip(
    "Holder",
    new Holder([new Coin(1, 2, 3)], new ImmutableValueDictionary<Extensible<AttributeName>, int>([KeyValuePair.Create((Extensible<AttributeName>)AttributeName.Power, 9)]), [[1, 2], []]),
    () => context.Holder,
    """{"Prices":[10203],"Attributes":{"Power":9},"Nested":[[1,2],[]]}"""
);

// Options are honored through the nested collection metadata
JsonSerializerOptions writeAsString = new() { TypeInfoResolver = context, NumberHandling = JsonNumberHandling.WriteAsString | JsonNumberHandling.AllowReadingFromString };
checks.Roundtrip(
    "IImmutableValueList<int> WriteAsString",
    new ImmutableValueList<int>([1]),
    () => (JsonTypeInfo<IImmutableValueList<int>>)writeAsString.GetTypeInfo(typeof(IImmutableValueList<int>)),
    """["1"]"""
);

// Reference-type values with a value-type key: created at runtime from the rooted canonical code
checks.Roundtrip(
    "IImmutableValueDictionary<int, Item>",
    new ImmutableValueDictionary<int, Item>([KeyValuePair.Create(1, new Item("z"))]),
    () => context.IImmutableValueDictionaryInt32Item,
    """{"1":{"Name":"z"}}"""
);

// Value types outside the precompiled set: Native AOT must fail with a clear message, the JIT works
checks.Expect(
    "IImmutableValueList<Point> (consumer struct)",
    () => JsonSerializer.Serialize(new ImmutableValueList<Point>([new Point(1, 2)]), context.IImmutableValueListPoint),
    native ? null : """[{"X":1,"Y":2}]"""
);
checks.Expect(
    "IImmutableValueList<AttributeName> (SDK enum not used in a collection by the SDK)",
    () => JsonSerializer.Serialize(new ImmutableValueList<AttributeName>([AttributeName.Power]), context.IImmutableValueListAttributeName),
    native ? null : """["Power"]"""
);
checks.Expect(
    "IImmutableValueDictionary<Extensible<AttributeName>, Coin> (pair not used by the SDK)",
    () => JsonSerializer.Serialize(
        new ImmutableValueDictionary<Extensible<AttributeName>, Coin>([KeyValuePair.Create((Extensible<AttributeName>)AttributeName.Power, new Coin(9))]),
        context.IImmutableValueDictionaryExtensibleAttributeNameCoin
    ),
    native ? null : """{"Power":9}"""
);

return checks.Report();

namespace GuildWars2.AotTests
{
    public sealed record Item(string Name);

    public readonly record struct Point(int X, int Y);

    public sealed record Holder(
        IImmutableValueList<Coin> Prices,
        IImmutableValueDictionary<Extensible<AttributeName>, int> Attributes,
        IImmutableValueList<IImmutableValueList<int>> Nested
    );

    internal sealed class Checks
    {
        private int failures;

        public void Roundtrip<T>(string name, T value, Func<JsonTypeInfo<T>> getTypeInfo, string expectedJson)
        {
            try
            {
                JsonTypeInfo<T> typeInfo = getTypeInfo();
                string json = JsonSerializer.Serialize(value, typeInfo);
                T? actual = JsonSerializer.Deserialize(json, typeInfo);
                string again = JsonSerializer.Serialize(actual!, typeInfo);
                if (json != expectedJson || again != expectedJson)
                {
                    Fail(name, $"expected {expectedJson}, wrote {json}, then {again}");
                    return;
                }

                Console.WriteLine($"PASS {name}");
            }
#pragma warning disable CA1031 // Report every failure
            catch (Exception exception)
#pragma warning restore CA1031
            {
                Fail(name, $"{exception.GetType().Name}: {exception.Message}");
            }
        }

        // expectedJson null: the action must throw NotSupportedException that mentions Native AOT
        public void Expect(string name, Func<string> action, string? expectedJson)
        {
            try
            {
                string json = action();
                if (json == expectedJson)
                {
                    Console.WriteLine($"PASS {name}");
                }
                else
                {
                    Fail(name, $"expected {expectedJson ?? "NotSupportedException"}, wrote {json}");
                }
            }
            catch (NotSupportedException exception) when (expectedJson is null && exception.Message.Contains("Native AOT", StringComparison.Ordinal))
            {
                Console.WriteLine($"PASS {name} (NotSupportedException: {exception.Message})");
            }
#pragma warning disable CA1031 // Report every failure
            catch (Exception exception)
#pragma warning restore CA1031
            {
                Fail(name, $"{exception.GetType().Name}: {exception.Message}");
            }
        }

        public int Report()
        {
            Console.WriteLine(failures == 0 ? "All checks passed." : $"{failures} check(s) failed.");
            return failures;
        }

        private void Fail(string name, string reason)
        {
            failures++;
            Console.WriteLine($"FAIL {name}: {reason}");
        }
    }

    [JsonSerializable(typeof(int))]
    [JsonSerializable(typeof(int?))]
    [JsonSerializable(typeof(long))]
    [JsonSerializable(typeof(double))]
    [JsonSerializable(typeof(decimal))]
    [JsonSerializable(typeof(bool))]
    [JsonSerializable(typeof(Guid))]
    [JsonSerializable(typeof(DateTimeOffset))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(Item))]
    [JsonSerializable(typeof(Point))]
    [JsonSerializable(typeof(IImmutableValueList<int>))]
    [JsonSerializable(typeof(ImmutableValueList<int>))]
    [JsonSerializable(typeof(ImmutableValueArray<int>))]
    [JsonSerializable(typeof(IImmutableValueSet<int>))]
    [JsonSerializable(typeof(ImmutableValueSet<int>))]
    [JsonSerializable(typeof(IImmutableValueList<int?>))]
    [JsonSerializable(typeof(IImmutableValueList<long>))]
    [JsonSerializable(typeof(IImmutableValueList<double>))]
    [JsonSerializable(typeof(IImmutableValueList<decimal>))]
    [JsonSerializable(typeof(IImmutableValueList<bool>))]
    [JsonSerializable(typeof(IImmutableValueList<Guid>))]
    [JsonSerializable(typeof(IImmutableValueList<DateTimeOffset>))]
    [JsonSerializable(typeof(IImmutableValueList<Coin>))]
    [JsonSerializable(typeof(IImmutableValueSet<Coin>))]
    [JsonSerializable(typeof(IImmutableValueList<Extensible<RaceName>>))]
    [JsonSerializable(typeof(IImmutableValueDictionary<Extensible<AttributeName>, Coin>))]
    [JsonSerializable(typeof(IImmutableValueList<Extensible<ProfessionName>>))]
    [JsonSerializable(typeof(IImmutableValueList<AttributeName>))]
    [JsonSerializable(typeof(IImmutableValueDictionary<int, int>))]
    [JsonSerializable(typeof(ImmutableValueDictionary<string, int>))]
    [JsonSerializable(typeof(IImmutableValueDictionary<int, string>))]
    [JsonSerializable(typeof(IImmutableValueDictionary<Extensible<AttributeName>, int>))]
    [JsonSerializable(typeof(IImmutableValueDictionary<string, Coin>))]
    [JsonSerializable(typeof(IImmutableValueList<string>))]
    [JsonSerializable(typeof(IImmutableValueList<DyeSlot>))]
    [JsonSerializable(typeof(IImmutableValueList<Item>))]
    [JsonSerializable(typeof(IImmutableValueList<IImmutableValueList<int>>))]
    [JsonSerializable(typeof(IImmutableValueDictionary<string, Item>))]
    [JsonSerializable(typeof(IImmutableValueList<Point>))]
    [JsonSerializable(typeof(IImmutableValueDictionary<int, Item>))]
    [JsonSerializable(typeof(Holder))]
    internal sealed partial class AotJsonContext : JsonSerializerContext;
}
