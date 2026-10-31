using System.Collections;
using System.Collections.Immutable;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

using GuildWars2.Collections;
using GuildWars2.Hero;
using GuildWars2.Hero.Equipment;

namespace GuildWars2.ArchitectureTests;

/// <summary>Combinatorial differential test for the immutable value collection converters.</summary>
/// <remarks>
/// <para>The oracle is what the converters did before https://github.com/sliekens/gw2sdk/issues/521: a verbatim copy
/// of main's converters (<see cref="LegacyImmutableValueCollectionJsonConverterFactory" />), which delegated to the
/// serializer for the equivalent System.Collections.Immutable interface, used with the reflection resolver and the same
/// options. It is the behavior consumers had (and persisted data with) when reflection was available.</para>
/// <para>Every combination of collection kind × element (or key/value) type × serializer options × type info resolver
/// must behave like the oracle, including with a source-generated context that has no reflection fallback: the same JSON when writing, and when reading, either an equivalent result (including
/// reference identity under <see cref="ReferenceHandler.Preserve" />) or the same exception type and JSON path.</para>
/// </remarks>
[Feature("Architecture")]
public class ImmutableValueCollectionJsonMatrixTest
{
    public static IEnumerable<Func<MatrixCase>> Cases()
    {
        return MatrixCases.All().Select(testCase => (Func<MatrixCase>)(() => testCase));
    }

    [Test]
    [MethodDataSource(nameof(Cases))]
    public async Task Behaves_like_the_previous_converters_with_reflection(MatrixCase testCase)
    {
        ArgumentNullException.ThrowIfNull(testCase);

        IReadOnlyList<string> discrepancies = testCase.Run();

        await Assert.That(string.Join(Environment.NewLine, discrepancies)).IsEmpty();
    }
}

public abstract class MatrixCase(string name)
{
    public abstract IReadOnlyList<string> Run();

    public override string ToString()
    {
        return name;
    }
}

internal static class MatrixCases
{
    private static readonly MatrixItem SharedItem = new() { Name = "shared" };

    private static readonly string[] ArrayInputs =
    [
        "null", "[]", "{}", "1", "\"a\"", "[null]", "[1,null]", "[\"1\"]", "[1", "[1,]", "[1/*c*/]", "[[]]", "[{}]",
        "[1.5]", "[\"NaN\"]", "[10203]", "[\"Power\"]", "[\"power\"]", "[\"Unknown\"]",
        """{"$id":"1","$values":[]}""",
        """[{"$id":"1","Name":"a"},{"$ref":"1"}]""",
        """[{"Name":"a","Extra":1}]""",
        """[{"$type":"d","A":1,"B":2}]""",
        """[{"color_id":1,"material":"cloth"}]"""
    ];

    private static readonly string[] ObjectInputs =
    [
        "null", "{}", "[]", "1", """{"1":1}""", """{"a":"b"}""", """{"1":null}""", """{"1":1,"1":2}""",
        """{"Power":1}""", """{"power":1}""", """{"POWER":1}""", """{"1":"1"}""", """{"$id":"1","1":1}""",
        """{"1":1,}""", """{"1":{"Name":"a"}}""", """{"1":[1]}""", """{"1":10203}""", """{"1":1""", """{"x":1}""",
        """{"1":{"$id":"1","Name":"a"},"2":{"$ref":"1"}}"""
    ];

    public static IEnumerable<MatrixCase> All()
    {
        return
        [
            .. ArrayCases<int>("int", [[], [1], [1, 2, 2, -3, int.MaxValue]]),
            .. ArrayCases<int?>("int?", [[null], [1, null]]),
            .. ArrayCases<long>("long", [[long.MaxValue, long.MinValue]]),
            .. ArrayCases<double>("double", [[1.5, 0.1], [double.NaN, double.PositiveInfinity]]),
            .. ArrayCases<decimal>("decimal", [[1.10m, -0.0001m]]),
            .. ArrayCases<string?>("string", [["a", null, "", "\u00e9\"<>&\u2028"]]),
            .. ArrayCases<object?>("object", [[1, "a", null, true]]),
            .. ArrayCases<AttributeName>("AttributeName", [[AttributeName.Power, (AttributeName)999]]),
            .. ArrayCases<Extensible<AttributeName>>("Extensible<AttributeName>", [[AttributeName.Power, "Unknown"]]),
            .. ArrayCases<Coin>("Coin", [[Coin.Zero, new Coin(1, 2, 3)]]),
            // Known gap: Nullable<T> of an SDK struct with an internal converter has no metadata in a consumer's context
            // and cannot be declared (SYSLIB1220). No SDK model uses one as an element. See #520.
            .. ArrayCases<Coin?>("Coin?", [[null, new Coin(5)]], unsupportedWith: "source-gen"),
            .. ArrayCases<DyeSlot?>("DyeSlot", [[new DyeSlot { ColorId = 1, Material = Material.Cloth }, null]]),
            .. ArrayCases<MatrixItem?>("MatrixItem", [[SharedItem, new MatrixItem { Name = "other" }, SharedItem], [null]]),
            .. ArrayCases<MatrixBase>("MatrixBase", [[new MatrixBase(1), new MatrixDerived(1, 2)]]),
            .. ArrayCases<IImmutableValueList<int>?>(
                "IImmutableValueList<int>",
                [[new ImmutableValueList<int>([1, 2]), ImmutableValueList<int>.Empty], [null]]
            ),
            HolderCase(),
            .. KeyCases<int>("int", 1, 2),
            .. KeyCases<string>("string", "a", "B"),
            .. KeyCases<AttributeName>("AttributeName", AttributeName.Power, AttributeName.Precision),
            .. KeyCases<Extensible<AttributeName>>("Extensible<AttributeName>", AttributeName.Power, "Unknown")
        ];
    }

    // Every collection kind as a member of an object, so the parent's serialization state (reference tracking, naming
    // policies, depth) surrounds the collections
    private static MatrixCase<MatrixHolder> HolderCase()
    {
        MatrixHolder populated = new()
        {
            Item = SharedItem,
            List = new ImmutableValueList<MatrixItem?>([SharedItem, null, SharedItem]),
            Array = [1, 2],
            Set = new ImmutableValueSet<string>(["a"]),
            ConcreteSet = [3],
            Coins = [new Coin(1, 2, 3)],
            Dictionary = new ImmutableValueDictionary<string, MatrixItem?>([KeyValuePair.Create("FooBar", (MatrixItem?)SharedItem)]),
            ConcreteDictionary = new ImmutableValueDictionary<int, int>([KeyValuePair.Create(1, 2)])
        };

        return new MatrixCase<MatrixHolder>(
            "MatrixHolder",
            [populated, new MatrixHolder()],
            static holder => (IEnumerable<object?>)[holder.Item, .. holder.List ?? [], .. holder.Dictionary?.Values ?? []],
            unordered: false,
            [
                .. ObjectInputs,
                """{"List":[{"$id":"1","Name":"a"},{"$ref":"1"}],"Item":{"$ref":"1"}}""",
                """{"Item":{"$id":"1","Name":"a"},"List":[{"$ref":"1"}]}""",
                """{"Array":["1"],"Set":[1],"ConcreteDictionary":{"1":"2"}}""",
                """{"list":[],"ARRAY":[1],"Unknown":[]}""",
                """{"Coins":[null]}"""
            ]
        );
    }

    private static IEnumerable<MatrixCase> KeyCases<TKey>(string key, TKey first, TKey second)
        where TKey : notnull
    {
        return
        [
            .. DictionaryCases<TKey, int>($"{key}, int", [[], [(first, 1), (second, -1)]]),
            .. DictionaryCases<TKey, string?>($"{key}, string", [[(first, "a"), (second, null)]]),
            .. DictionaryCases<TKey, object?>($"{key}, object", [[(first, 1), (second, null)]]),
            .. DictionaryCases<TKey, Coin>($"{key}, Coin", [[(first, new Coin(1, 2, 3))]]),
            .. DictionaryCases<TKey, MatrixItem?>($"{key}, MatrixItem", [[(first, SharedItem), (second, SharedItem)]]),
            .. DictionaryCases<TKey, IImmutableValueList<int>?>(
                $"{key}, IImmutableValueList<int>",
                [[(first, new ImmutableValueList<int>([1])), (second, null)]]
            )
        ];
    }

    private static IEnumerable<MatrixCase> ArrayCases<T>(string element, T[][] samples, string? unsupportedWith = null)
    {
        return
        [
            new MatrixCase<IImmutableValueList<T>>(
                $"IImmutableValueList<{element}>",
                [.. samples.Select(static items => (IImmutableValueList<T>)new ImmutableValueList<T>(items))],
                static value => value,
                unordered: false,
                ArrayInputs,
                unsupportedWith
            ),
            new MatrixCase<ImmutableValueList<T>>(
                $"ImmutableValueList<{element}>",
                [.. samples.Select(static items => new ImmutableValueList<T>(items))],
                static value => value,
                unordered: false,
                ArrayInputs,
                unsupportedWith
            ),
            new MatrixCase<ImmutableValueArray<T>>(
                $"ImmutableValueArray<{element}>",
                [.. samples.Select(static items => new ImmutableValueArray<T>(items))],
                static value => value,
                unordered: false,
                ArrayInputs,
                unsupportedWith
            ),
            new MatrixCase<IImmutableValueSet<T>>(
                $"IImmutableValueSet<{element}>",
                [.. samples.Select(static items => (IImmutableValueSet<T>)new ImmutableValueSet<T>(items))],
                static value => value,
                unordered: true,
                ArrayInputs,
                unsupportedWith
            ),
            new MatrixCase<ImmutableValueSet<T>>(
                $"ImmutableValueSet<{element}>",
                [.. samples.Select(static items => new ImmutableValueSet<T>(items))],
                static value => value,
                unordered: true,
                ArrayInputs,
                unsupportedWith
            )
        ];
    }

    private static IEnumerable<MatrixCase> DictionaryCases<TKey, TValue>(string types, (TKey Key, TValue Value)[][] samples)
        where TKey : notnull
    {
        static ImmutableValueDictionary<TKey, TValue> Create((TKey Key, TValue Value)[] pairs)
        {
            return new ImmutableValueDictionary<TKey, TValue>(pairs.Select(static pair => KeyValuePair.Create(pair.Key, pair.Value)));
        }

        return
        [
            new MatrixCase<IImmutableValueDictionary<TKey, TValue>>(
                $"IImmutableValueDictionary<{types}>",
                [.. samples.Select(static pairs => (IImmutableValueDictionary<TKey, TValue>)Create(pairs))],
                static value => value.Values,
                unordered: false,
                ObjectInputs
            ),
            new MatrixCase<ImmutableValueDictionary<TKey, TValue>>(
                $"ImmutableValueDictionary<{types}>",
                [.. samples.Select(Create)],
                static value => value.Values,
                unordered: false,
                ObjectInputs
            )
        ];
    }
}

internal static class MatrixOptions
{
    public static readonly (string Name, Action<JsonSerializerOptions> Configure)[] Variants =
    [
        ("Default", static _ => { }),
        ("AllowReadingFromString", static options => options.NumberHandling = JsonNumberHandling.AllowReadingFromString),
        ("WriteAsString", static options => options.NumberHandling = JsonNumberHandling.WriteAsString),
        ("AllowNamedFloatingPointLiterals", static options => options.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals),
        ("Preserve", static options => options.ReferenceHandler = ReferenceHandler.Preserve),
        ("IgnoreCycles", static options => options.ReferenceHandler = ReferenceHandler.IgnoreCycles),
        ("CamelCase", static options =>
        {
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        }),
        ("PropertyNameCaseInsensitive", static options => options.PropertyNameCaseInsensitive = true),
        ("WhenWritingNull", static options => options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull),
        ("JsonStringEnumConverter", static options => options.Converters.Add(new JsonStringEnumConverter())),
        ("DisallowUnmappedMembers", static options => options.UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow),
        ("Lenient", static options =>
        {
            options.AllowTrailingCommas = true;
            options.ReadCommentHandling = JsonCommentHandling.Skip;
        }),
        ("DisallowDuplicateProperties", static options => options.AllowDuplicateProperties = false),
        ("MaxDepth2", static options => options.MaxDepth = 2),
        ("RespectNullableAnnotations", static options => options.RespectNullableAnnotations = true),
        ("RelaxedEscaping", static options => options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping),
        ("WriteIndented", static options => options.WriteIndented = true)
    ];

    // Each subject resolver is paired with the resolver the oracle uses. For source generation that is a context which
    // declares the System.Collections.Immutable interfaces, the workaround the previous converters required.
    public static readonly (string Name, Func<IJsonTypeInfoResolver> Create, Func<IJsonTypeInfoResolver> CreateOracle)[] Resolvers =
    [
        ("reflection", static () => new DefaultJsonTypeInfoResolver(), static () => new DefaultJsonTypeInfoResolver()),
        ("source-gen", static () => MatrixJsonContext.Default, static () => LegacyMatrixJsonContext.Default),
        (
            "source-gen+reflection",
            static () => JsonTypeInfoResolver.Combine(MatrixJsonContext.Default, new DefaultJsonTypeInfoResolver()),
            static () => JsonTypeInfoResolver.Combine(LegacyMatrixJsonContext.Default, new DefaultJsonTypeInfoResolver())
        )
    ];
}

internal sealed partial class MatrixCase<TSubject>(
    string name,
    TSubject[] samples,
    Func<TSubject, IEnumerable> elements,
    bool unordered,
    string[] inputs,
    string? unsupportedWith = null
) : MatrixCase(name)
    where TSubject : class
{
    public override IReadOnlyList<string> Run()
    {
        List<string> discrepancies = [];
        foreach ((string optionsName, Action<JsonSerializerOptions> configure) in MatrixOptions.Variants)
        {
            JsonSerializerOptions reflectionOracleOptions = OracleOptions(new DefaultJsonTypeInfoResolver(), configure);
            foreach ((string resolverName, Func<IJsonTypeInfoResolver> createResolver, Func<IJsonTypeInfoResolver> createOracleResolver) in MatrixOptions.Resolvers)
            {
                JsonSerializerOptions subjectOptions = new() { TypeInfoResolver = createResolver() };

                // Any registered converter disables the source-generated fast path, which skips MaxDepth checks. The
                // oracle needs a registered converter, so the subject gets an inert one to compare like with like. The
                // fast path is covered separately in ImmutableValueCollectionJsonTest.
                subjectOptions.Converters.Add(new InertJsonConverter());
                configure(subjectOptions);
                subjectOptions.MakeReadOnly();

                JsonSerializerOptions oracleOptions = OracleOptions(createOracleResolver(), configure);

                // What the previous converters did with this resolver; where they could not work at all (the bug), what
                // they did with reflection
                Outcome Expect(Func<JsonSerializerOptions, string> action)
                {
                    Outcome outcome = Outcome.Of(() => action(oracleOptions));
                    return outcome.Error == nameof(NotSupportedException)
                        ? Outcome.Of(() => action(reflectionOracleOptions))
                        : outcome;
                }

                string context = $"[{optionsName}, {resolverName}]";
                List<string> jsonInputs = [.. inputs];

                foreach (TSubject? sample in samples.Append(null))
                {
                    Outcome expected = Expect(options => JsonSerializer.Serialize(sample, options));
                    Outcome actual = Outcome.Of(() => JsonSerializer.Serialize(sample, subjectOptions));
                    if (!Same(expected, actual) && !KnownGap(resolverName, actual))
                    {
                        discrepancies.Add($"{context} write: expected {expected}, got {actual}");
                    }

                    if (expected.Json is not null)
                    {
                        jsonInputs.Add(expected.Json);
                    }
                }

                foreach (string json in jsonInputs.Distinct())
                {
                    TSubject? expectedValue = null;
                    TSubject? actualValue = null;
                    Outcome expected = Expect(options => Canonical(expectedValue = JsonSerializer.Deserialize<TSubject>(json, options), reflectionOracleOptions));
                    Outcome actual = Outcome.Of(() => Canonical(actualValue = JsonSerializer.Deserialize<TSubject>(json, subjectOptions), reflectionOracleOptions));
                    if (!Same(expected, actual) && !KnownGap(resolverName, actual))
                    {
                        discrepancies.Add($"{context} read {json}: expected {expected}, got {actual}");
                    }
                    else if (!unordered && expectedValue is not null && actualValue is not null)
                    {
                        string expectedIdentity = Identity(elements(expectedValue));
                        string actualIdentity = Identity(elements(actualValue));
                        if (expectedIdentity != actualIdentity)
                        {
                            discrepancies.Add($"{context} read {json}: expected reference identity {expectedIdentity}, got {actualIdentity}");
                        }
                    }
                }
            }
        }

        return discrepancies;
    }

    // With a resolver that cannot provide the element metadata, failing with NotSupportedException is the expected outcome
    private bool KnownGap(string resolverName, Outcome actual)
    {
        return resolverName == unsupportedWith && actual.Error == nameof(NotSupportedException);
    }

    private static JsonSerializerOptions OracleOptions(IJsonTypeInfoResolver resolver, Action<JsonSerializerOptions> configure)
    {
        JsonSerializerOptions options = new() { TypeInfoResolver = resolver };
        options.Converters.Add(new LegacyImmutableValueCollectionJsonConverterFactory());
        configure(options);
        options.MakeReadOnly();
        return options;
    }

    private bool Same(Outcome expected, Outcome actual)
    {
        return unordered && expected.Json is not null && actual.Json is not null
            ? SortedElements(expected.Json) == SortedElements(actual.Json)
            : expected == actual;
    }

    // Hash set enumeration order depends on element hash codes, which differ between instances of reference types. The
    // $id values follow the enumeration order, so they are ignored too.
    private static string SortedElements(string json)
    {
        if (!json.StartsWith('[', StringComparison.Ordinal))
        {
            return json;
        }

        using JsonDocument document = JsonDocument.Parse(json);
        return document.RootElement.ValueKind == JsonValueKind.Array
            ? string.Join(",", document.RootElement.EnumerateArray().Select(static element => PreserveId().Replace(element.GetRawText(), "\"$id\":\"#\"")).Order(StringComparer.Ordinal))
            : json;
    }

    // Serializes a read result with the oracle, so results from both sides are compared the same way
    private static string Canonical(TSubject? value, JsonSerializerOptions options)
    {
        try
        {
            return JsonSerializer.Serialize(value, options);
        }
#pragma warning disable CA1031 // Any exception is part of the canonical form
        catch (Exception exception)
#pragma warning restore CA1031
        {
            return $"<unserializable: {exception.GetType().Name}>";
        }
    }

    [System.Text.RegularExpressions.GeneratedRegex("\"\\$id\":\"\\d+\"")]
    private static partial System.Text.RegularExpressions.Regex PreserveId();

    // For each pair of elements, whether they are the same instance
    private static string Identity(IEnumerable values)
    {
        object?[] items = [.. values.Cast<object?>()];
        System.Text.StringBuilder pattern = new();
        for (int i = 0; i < items.Length; i++)
        {
            for (int j = i + 1; j < items.Length; j++)
            {
                bool same = items[i] is { } item && !item.GetType().IsValueType && ReferenceEquals(item, items[j]);
                _ = pattern.Append(same ? '1' : '0');
            }
        }

        return pattern.ToString();
    }
}

internal readonly struct Outcome(string? json, string? error, string? path, string? message) : IEquatable<Outcome>
{
    public string? Json => json;

    public string? Error => error;

    public string? Path => path;

    public static Outcome Of(Func<string> action)
    {
        try
        {
            return new Outcome(action(), null, null, null);
        }
#pragma warning disable CA1031 // Any exception is an outcome to compare with the oracle
        catch (Exception exception)
#pragma warning restore CA1031
        {
            return new Outcome(null, exception.GetType().Name, (exception as JsonException)?.Path, exception.Message);
        }
    }

    // The message is diagnostic only: it is not compared
    public bool Equals(Outcome other)
    {
        return (json, error, path) == (other.Json, other.Error, other.Path);
    }

    public override bool Equals(object? obj)
    {
        return obj is Outcome other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(json, error, path);
    }

    public static bool operator ==(Outcome left, Outcome right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Outcome left, Outcome right)
    {
        return !left.Equals(right);
    }

    public override string ToString()
    {
        return error is null ? json! : $"{error} at {path ?? "<no path>"} ({message})";
    }
}

/// <summary>Converts nothing; registering it disables the source-generated fast path.</summary>
internal sealed class InertJsonConverter : JsonConverter<InertJsonConverter>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return false;
    }

    public override InertJsonConverter Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotSupportedException();
    }

    public override void Write(Utf8JsonWriter writer, InertJsonConverter value, JsonSerializerOptions options)
    {
        throw new NotSupportedException();
    }
}

public sealed class MatrixHolder
{
    public MatrixItem? Item { get; set; }

    public IImmutableValueList<MatrixItem?>? List { get; set; }

    public ImmutableValueArray<int>? Array { get; set; }

    public IImmutableValueSet<string>? Set { get; set; }

    public ImmutableValueSet<int>? ConcreteSet { get; set; }

    public ImmutableValueList<Coin>? Coins { get; set; }

    public IImmutableValueDictionary<string, MatrixItem?>? Dictionary { get; set; }

    public ImmutableValueDictionary<int, int>? ConcreteDictionary { get; set; }
}

public sealed class MatrixItem
{
    public string? Name { get; set; }
}

[JsonDerivedType(typeof(MatrixDerived), "d")]
public record MatrixBase(int A);

public sealed record MatrixDerived(int A, int B) : MatrixBase(A);

// Element types are declared where a consumer can declare them. Coin, Coin?, Extensible<T>, DyeSlot and SDK enums
// such as AttributeName cannot be declared: their [JsonConverter] is internal (SYSLIB1220/SYSLIB1030, see #520).
[JsonSerializable(typeof(int))]
[JsonSerializable(typeof(int?))]
[JsonSerializable(typeof(long))]
[JsonSerializable(typeof(double))]
[JsonSerializable(typeof(decimal))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(object))]
[JsonSerializable(typeof(bool))]
[JsonSerializable(typeof(MatrixItem))]
[JsonSerializable(typeof(MatrixBase))]
[JsonSerializable(typeof(MatrixDerived))]
[JsonSerializable(typeof(IImmutableValueList<int>))]
[JsonSerializable(typeof(ImmutableValueList<int>))]
[JsonSerializable(typeof(ImmutableValueArray<int>))]
[JsonSerializable(typeof(IImmutableValueSet<int>))]
[JsonSerializable(typeof(ImmutableValueSet<int>))]
[JsonSerializable(typeof(IImmutableValueList<int?>))]
[JsonSerializable(typeof(ImmutableValueList<int?>))]
[JsonSerializable(typeof(ImmutableValueArray<int?>))]
[JsonSerializable(typeof(IImmutableValueSet<int?>))]
[JsonSerializable(typeof(ImmutableValueSet<int?>))]
[JsonSerializable(typeof(IImmutableValueList<long>))]
[JsonSerializable(typeof(ImmutableValueList<long>))]
[JsonSerializable(typeof(ImmutableValueArray<long>))]
[JsonSerializable(typeof(IImmutableValueSet<long>))]
[JsonSerializable(typeof(ImmutableValueSet<long>))]
[JsonSerializable(typeof(IImmutableValueList<double>))]
[JsonSerializable(typeof(ImmutableValueList<double>))]
[JsonSerializable(typeof(ImmutableValueArray<double>))]
[JsonSerializable(typeof(IImmutableValueSet<double>))]
[JsonSerializable(typeof(ImmutableValueSet<double>))]
[JsonSerializable(typeof(IImmutableValueList<decimal>))]
[JsonSerializable(typeof(ImmutableValueList<decimal>))]
[JsonSerializable(typeof(ImmutableValueArray<decimal>))]
[JsonSerializable(typeof(IImmutableValueSet<decimal>))]
[JsonSerializable(typeof(ImmutableValueSet<decimal>))]
[JsonSerializable(typeof(IImmutableValueList<string>))]
[JsonSerializable(typeof(ImmutableValueList<string>))]
[JsonSerializable(typeof(ImmutableValueArray<string>))]
[JsonSerializable(typeof(IImmutableValueSet<string>))]
[JsonSerializable(typeof(ImmutableValueSet<string>))]
[JsonSerializable(typeof(IImmutableValueList<object>))]
[JsonSerializable(typeof(ImmutableValueList<object>))]
[JsonSerializable(typeof(ImmutableValueArray<object>))]
[JsonSerializable(typeof(IImmutableValueSet<object>))]
[JsonSerializable(typeof(ImmutableValueSet<object>))]
[JsonSerializable(typeof(IImmutableValueList<AttributeName>))]
[JsonSerializable(typeof(ImmutableValueList<AttributeName>))]
[JsonSerializable(typeof(ImmutableValueArray<AttributeName>))]
[JsonSerializable(typeof(IImmutableValueSet<AttributeName>))]
[JsonSerializable(typeof(ImmutableValueSet<AttributeName>))]
[JsonSerializable(typeof(IImmutableValueList<Extensible<AttributeName>>))]
[JsonSerializable(typeof(ImmutableValueList<Extensible<AttributeName>>))]
[JsonSerializable(typeof(ImmutableValueArray<Extensible<AttributeName>>))]
[JsonSerializable(typeof(IImmutableValueSet<Extensible<AttributeName>>))]
[JsonSerializable(typeof(ImmutableValueSet<Extensible<AttributeName>>))]
[JsonSerializable(typeof(IImmutableValueList<Coin>))]
[JsonSerializable(typeof(ImmutableValueList<Coin>))]
[JsonSerializable(typeof(ImmutableValueArray<Coin>))]
[JsonSerializable(typeof(IImmutableValueSet<Coin>))]
[JsonSerializable(typeof(ImmutableValueSet<Coin>))]
[JsonSerializable(typeof(IImmutableValueList<Coin?>))]
[JsonSerializable(typeof(ImmutableValueList<Coin?>))]
[JsonSerializable(typeof(ImmutableValueArray<Coin?>))]
[JsonSerializable(typeof(IImmutableValueSet<Coin?>))]
[JsonSerializable(typeof(ImmutableValueSet<Coin?>))]
[JsonSerializable(typeof(IImmutableValueList<DyeSlot>))]
[JsonSerializable(typeof(ImmutableValueList<DyeSlot>))]
[JsonSerializable(typeof(ImmutableValueArray<DyeSlot>))]
[JsonSerializable(typeof(IImmutableValueSet<DyeSlot>))]
[JsonSerializable(typeof(ImmutableValueSet<DyeSlot>))]
[JsonSerializable(typeof(IImmutableValueList<MatrixItem>))]
[JsonSerializable(typeof(ImmutableValueList<MatrixItem>))]
[JsonSerializable(typeof(ImmutableValueArray<MatrixItem>))]
[JsonSerializable(typeof(IImmutableValueSet<MatrixItem>))]
[JsonSerializable(typeof(ImmutableValueSet<MatrixItem>))]
[JsonSerializable(typeof(IImmutableValueList<MatrixBase>))]
[JsonSerializable(typeof(ImmutableValueList<MatrixBase>))]
[JsonSerializable(typeof(ImmutableValueArray<MatrixBase>))]
[JsonSerializable(typeof(IImmutableValueSet<MatrixBase>))]
[JsonSerializable(typeof(ImmutableValueSet<MatrixBase>))]
[JsonSerializable(typeof(IImmutableValueList<IImmutableValueList<int>>))]
[JsonSerializable(typeof(ImmutableValueList<IImmutableValueList<int>>))]
[JsonSerializable(typeof(ImmutableValueArray<IImmutableValueList<int>>))]
[JsonSerializable(typeof(IImmutableValueSet<IImmutableValueList<int>>))]
[JsonSerializable(typeof(ImmutableValueSet<IImmutableValueList<int>>))]
[JsonSerializable(typeof(IImmutableValueDictionary<int, int>))]
[JsonSerializable(typeof(ImmutableValueDictionary<int, int>))]
[JsonSerializable(typeof(IImmutableValueDictionary<int, string>))]
[JsonSerializable(typeof(ImmutableValueDictionary<int, string>))]
[JsonSerializable(typeof(IImmutableValueDictionary<int, object>))]
[JsonSerializable(typeof(ImmutableValueDictionary<int, object>))]
[JsonSerializable(typeof(IImmutableValueDictionary<int, Coin>))]
[JsonSerializable(typeof(ImmutableValueDictionary<int, Coin>))]
[JsonSerializable(typeof(IImmutableValueDictionary<int, MatrixItem>))]
[JsonSerializable(typeof(ImmutableValueDictionary<int, MatrixItem>))]
[JsonSerializable(typeof(IImmutableValueDictionary<int, IImmutableValueList<int>>))]
[JsonSerializable(typeof(ImmutableValueDictionary<int, IImmutableValueList<int>>))]
[JsonSerializable(typeof(IImmutableValueDictionary<string, int>))]
[JsonSerializable(typeof(ImmutableValueDictionary<string, int>))]
[JsonSerializable(typeof(IImmutableValueDictionary<string, string>))]
[JsonSerializable(typeof(ImmutableValueDictionary<string, string>))]
[JsonSerializable(typeof(IImmutableValueDictionary<string, object>))]
[JsonSerializable(typeof(ImmutableValueDictionary<string, object>))]
[JsonSerializable(typeof(IImmutableValueDictionary<string, Coin>))]
[JsonSerializable(typeof(ImmutableValueDictionary<string, Coin>))]
[JsonSerializable(typeof(IImmutableValueDictionary<string, MatrixItem>))]
[JsonSerializable(typeof(ImmutableValueDictionary<string, MatrixItem>))]
[JsonSerializable(typeof(IImmutableValueDictionary<string, IImmutableValueList<int>>))]
[JsonSerializable(typeof(ImmutableValueDictionary<string, IImmutableValueList<int>>))]
[JsonSerializable(typeof(IImmutableValueDictionary<AttributeName, int>))]
[JsonSerializable(typeof(ImmutableValueDictionary<AttributeName, int>))]
[JsonSerializable(typeof(IImmutableValueDictionary<AttributeName, string>))]
[JsonSerializable(typeof(ImmutableValueDictionary<AttributeName, string>))]
[JsonSerializable(typeof(IImmutableValueDictionary<AttributeName, object>))]
[JsonSerializable(typeof(ImmutableValueDictionary<AttributeName, object>))]
[JsonSerializable(typeof(IImmutableValueDictionary<AttributeName, Coin>))]
[JsonSerializable(typeof(ImmutableValueDictionary<AttributeName, Coin>))]
[JsonSerializable(typeof(IImmutableValueDictionary<AttributeName, MatrixItem>))]
[JsonSerializable(typeof(ImmutableValueDictionary<AttributeName, MatrixItem>))]
[JsonSerializable(typeof(IImmutableValueDictionary<AttributeName, IImmutableValueList<int>>))]
[JsonSerializable(typeof(ImmutableValueDictionary<AttributeName, IImmutableValueList<int>>))]
[JsonSerializable(typeof(IImmutableValueDictionary<Extensible<AttributeName>, int>))]
[JsonSerializable(typeof(ImmutableValueDictionary<Extensible<AttributeName>, int>))]
[JsonSerializable(typeof(IImmutableValueDictionary<Extensible<AttributeName>, string>))]
[JsonSerializable(typeof(ImmutableValueDictionary<Extensible<AttributeName>, string>))]
[JsonSerializable(typeof(IImmutableValueDictionary<Extensible<AttributeName>, object>))]
[JsonSerializable(typeof(ImmutableValueDictionary<Extensible<AttributeName>, object>))]
[JsonSerializable(typeof(IImmutableValueDictionary<Extensible<AttributeName>, Coin>))]
[JsonSerializable(typeof(ImmutableValueDictionary<Extensible<AttributeName>, Coin>))]
[JsonSerializable(typeof(IImmutableValueDictionary<Extensible<AttributeName>, MatrixItem>))]
[JsonSerializable(typeof(ImmutableValueDictionary<Extensible<AttributeName>, MatrixItem>))]
[JsonSerializable(typeof(IImmutableValueDictionary<Extensible<AttributeName>, IImmutableValueList<int>>))]
[JsonSerializable(typeof(ImmutableValueDictionary<Extensible<AttributeName>, IImmutableValueList<int>>))]
[JsonSerializable(typeof(MatrixHolder))]
internal sealed partial class MatrixJsonContext : JsonSerializerContext;

// The workaround the previous converters needed with source generation: metadata for the System.Collections.Immutable
// interfaces they delegated to (as well as for the collection types themselves). It cannot be declared for SDK element
// types with internal converters (SYSLIB1220).
[JsonSerializable(typeof(int))]
[JsonSerializable(typeof(int?))]
[JsonSerializable(typeof(long))]
[JsonSerializable(typeof(double))]
[JsonSerializable(typeof(decimal))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(object))]
[JsonSerializable(typeof(bool))]
[JsonSerializable(typeof(MatrixItem))]
[JsonSerializable(typeof(MatrixBase))]
[JsonSerializable(typeof(MatrixDerived))]
[JsonSerializable(typeof(IImmutableValueList<int>))]
[JsonSerializable(typeof(IImmutableList<int>))]
[JsonSerializable(typeof(IImmutableSet<int>))]
[JsonSerializable(typeof(IImmutableList<int?>))]
[JsonSerializable(typeof(IImmutableSet<int?>))]
[JsonSerializable(typeof(IImmutableList<long>))]
[JsonSerializable(typeof(IImmutableSet<long>))]
[JsonSerializable(typeof(IImmutableList<double>))]
[JsonSerializable(typeof(IImmutableSet<double>))]
[JsonSerializable(typeof(IImmutableList<decimal>))]
[JsonSerializable(typeof(IImmutableSet<decimal>))]
[JsonSerializable(typeof(IImmutableList<string>))]
[JsonSerializable(typeof(IImmutableSet<string>))]
[JsonSerializable(typeof(IImmutableList<object>))]
[JsonSerializable(typeof(IImmutableSet<object>))]
[JsonSerializable(typeof(IImmutableList<MatrixItem>))]
[JsonSerializable(typeof(IImmutableSet<MatrixItem>))]
[JsonSerializable(typeof(IImmutableList<MatrixBase>))]
[JsonSerializable(typeof(IImmutableSet<MatrixBase>))]
[JsonSerializable(typeof(IImmutableList<IImmutableValueList<int>>))]
[JsonSerializable(typeof(IImmutableSet<IImmutableValueList<int>>))]
[JsonSerializable(typeof(IImmutableDictionary<int, int>))]
[JsonSerializable(typeof(IImmutableDictionary<int, string>))]
[JsonSerializable(typeof(IImmutableDictionary<int, object>))]
[JsonSerializable(typeof(IImmutableDictionary<int, MatrixItem>))]
[JsonSerializable(typeof(IImmutableDictionary<int, IImmutableValueList<int>>))]
[JsonSerializable(typeof(IImmutableDictionary<string, int>))]
[JsonSerializable(typeof(IImmutableDictionary<string, string>))]
[JsonSerializable(typeof(IImmutableDictionary<string, object>))]
[JsonSerializable(typeof(IImmutableDictionary<string, MatrixItem>))]
[JsonSerializable(typeof(IImmutableDictionary<string, IImmutableValueList<int>>))]
[JsonSerializable(typeof(ImmutableValueList<int>))]
[JsonSerializable(typeof(ImmutableValueArray<int>))]
[JsonSerializable(typeof(IImmutableValueSet<int>))]
[JsonSerializable(typeof(ImmutableValueSet<int>))]
[JsonSerializable(typeof(IImmutableValueList<int?>))]
[JsonSerializable(typeof(ImmutableValueList<int?>))]
[JsonSerializable(typeof(ImmutableValueArray<int?>))]
[JsonSerializable(typeof(IImmutableValueSet<int?>))]
[JsonSerializable(typeof(ImmutableValueSet<int?>))]
[JsonSerializable(typeof(IImmutableValueList<long>))]
[JsonSerializable(typeof(ImmutableValueList<long>))]
[JsonSerializable(typeof(ImmutableValueArray<long>))]
[JsonSerializable(typeof(IImmutableValueSet<long>))]
[JsonSerializable(typeof(ImmutableValueSet<long>))]
[JsonSerializable(typeof(IImmutableValueList<double>))]
[JsonSerializable(typeof(ImmutableValueList<double>))]
[JsonSerializable(typeof(ImmutableValueArray<double>))]
[JsonSerializable(typeof(IImmutableValueSet<double>))]
[JsonSerializable(typeof(ImmutableValueSet<double>))]
[JsonSerializable(typeof(IImmutableValueList<decimal>))]
[JsonSerializable(typeof(ImmutableValueList<decimal>))]
[JsonSerializable(typeof(ImmutableValueArray<decimal>))]
[JsonSerializable(typeof(IImmutableValueSet<decimal>))]
[JsonSerializable(typeof(ImmutableValueSet<decimal>))]
[JsonSerializable(typeof(IImmutableValueList<string>))]
[JsonSerializable(typeof(ImmutableValueList<string>))]
[JsonSerializable(typeof(ImmutableValueArray<string>))]
[JsonSerializable(typeof(IImmutableValueSet<string>))]
[JsonSerializable(typeof(ImmutableValueSet<string>))]
[JsonSerializable(typeof(IImmutableValueList<object>))]
[JsonSerializable(typeof(ImmutableValueList<object>))]
[JsonSerializable(typeof(ImmutableValueArray<object>))]
[JsonSerializable(typeof(IImmutableValueSet<object>))]
[JsonSerializable(typeof(ImmutableValueSet<object>))]
[JsonSerializable(typeof(IImmutableValueList<AttributeName>))]
[JsonSerializable(typeof(ImmutableValueList<AttributeName>))]
[JsonSerializable(typeof(ImmutableValueArray<AttributeName>))]
[JsonSerializable(typeof(IImmutableValueSet<AttributeName>))]
[JsonSerializable(typeof(ImmutableValueSet<AttributeName>))]
[JsonSerializable(typeof(IImmutableValueList<Extensible<AttributeName>>))]
[JsonSerializable(typeof(ImmutableValueList<Extensible<AttributeName>>))]
[JsonSerializable(typeof(ImmutableValueArray<Extensible<AttributeName>>))]
[JsonSerializable(typeof(IImmutableValueSet<Extensible<AttributeName>>))]
[JsonSerializable(typeof(ImmutableValueSet<Extensible<AttributeName>>))]
[JsonSerializable(typeof(IImmutableValueList<Coin>))]
[JsonSerializable(typeof(ImmutableValueList<Coin>))]
[JsonSerializable(typeof(ImmutableValueArray<Coin>))]
[JsonSerializable(typeof(IImmutableValueSet<Coin>))]
[JsonSerializable(typeof(ImmutableValueSet<Coin>))]
[JsonSerializable(typeof(IImmutableValueList<Coin?>))]
[JsonSerializable(typeof(ImmutableValueList<Coin?>))]
[JsonSerializable(typeof(ImmutableValueArray<Coin?>))]
[JsonSerializable(typeof(IImmutableValueSet<Coin?>))]
[JsonSerializable(typeof(ImmutableValueSet<Coin?>))]
[JsonSerializable(typeof(IImmutableValueList<DyeSlot>))]
[JsonSerializable(typeof(ImmutableValueList<DyeSlot>))]
[JsonSerializable(typeof(ImmutableValueArray<DyeSlot>))]
[JsonSerializable(typeof(IImmutableValueSet<DyeSlot>))]
[JsonSerializable(typeof(ImmutableValueSet<DyeSlot>))]
[JsonSerializable(typeof(IImmutableValueList<MatrixItem>))]
[JsonSerializable(typeof(ImmutableValueList<MatrixItem>))]
[JsonSerializable(typeof(ImmutableValueArray<MatrixItem>))]
[JsonSerializable(typeof(IImmutableValueSet<MatrixItem>))]
[JsonSerializable(typeof(ImmutableValueSet<MatrixItem>))]
[JsonSerializable(typeof(IImmutableValueList<MatrixBase>))]
[JsonSerializable(typeof(ImmutableValueList<MatrixBase>))]
[JsonSerializable(typeof(ImmutableValueArray<MatrixBase>))]
[JsonSerializable(typeof(IImmutableValueSet<MatrixBase>))]
[JsonSerializable(typeof(ImmutableValueSet<MatrixBase>))]
[JsonSerializable(typeof(IImmutableValueList<IImmutableValueList<int>>))]
[JsonSerializable(typeof(ImmutableValueList<IImmutableValueList<int>>))]
[JsonSerializable(typeof(ImmutableValueArray<IImmutableValueList<int>>))]
[JsonSerializable(typeof(IImmutableValueSet<IImmutableValueList<int>>))]
[JsonSerializable(typeof(ImmutableValueSet<IImmutableValueList<int>>))]
[JsonSerializable(typeof(IImmutableValueDictionary<int, int>))]
[JsonSerializable(typeof(ImmutableValueDictionary<int, int>))]
[JsonSerializable(typeof(IImmutableValueDictionary<int, string>))]
[JsonSerializable(typeof(ImmutableValueDictionary<int, string>))]
[JsonSerializable(typeof(IImmutableValueDictionary<int, object>))]
[JsonSerializable(typeof(ImmutableValueDictionary<int, object>))]
[JsonSerializable(typeof(IImmutableValueDictionary<int, Coin>))]
[JsonSerializable(typeof(ImmutableValueDictionary<int, Coin>))]
[JsonSerializable(typeof(IImmutableValueDictionary<int, MatrixItem>))]
[JsonSerializable(typeof(ImmutableValueDictionary<int, MatrixItem>))]
[JsonSerializable(typeof(IImmutableValueDictionary<int, IImmutableValueList<int>>))]
[JsonSerializable(typeof(ImmutableValueDictionary<int, IImmutableValueList<int>>))]
[JsonSerializable(typeof(IImmutableValueDictionary<string, int>))]
[JsonSerializable(typeof(ImmutableValueDictionary<string, int>))]
[JsonSerializable(typeof(IImmutableValueDictionary<string, string>))]
[JsonSerializable(typeof(ImmutableValueDictionary<string, string>))]
[JsonSerializable(typeof(IImmutableValueDictionary<string, object>))]
[JsonSerializable(typeof(ImmutableValueDictionary<string, object>))]
[JsonSerializable(typeof(IImmutableValueDictionary<string, Coin>))]
[JsonSerializable(typeof(ImmutableValueDictionary<string, Coin>))]
[JsonSerializable(typeof(IImmutableValueDictionary<string, MatrixItem>))]
[JsonSerializable(typeof(ImmutableValueDictionary<string, MatrixItem>))]
[JsonSerializable(typeof(IImmutableValueDictionary<string, IImmutableValueList<int>>))]
[JsonSerializable(typeof(ImmutableValueDictionary<string, IImmutableValueList<int>>))]
[JsonSerializable(typeof(IImmutableValueDictionary<AttributeName, int>))]
[JsonSerializable(typeof(ImmutableValueDictionary<AttributeName, int>))]
[JsonSerializable(typeof(IImmutableValueDictionary<AttributeName, string>))]
[JsonSerializable(typeof(ImmutableValueDictionary<AttributeName, string>))]
[JsonSerializable(typeof(IImmutableValueDictionary<AttributeName, object>))]
[JsonSerializable(typeof(ImmutableValueDictionary<AttributeName, object>))]
[JsonSerializable(typeof(IImmutableValueDictionary<AttributeName, Coin>))]
[JsonSerializable(typeof(ImmutableValueDictionary<AttributeName, Coin>))]
[JsonSerializable(typeof(IImmutableValueDictionary<AttributeName, MatrixItem>))]
[JsonSerializable(typeof(ImmutableValueDictionary<AttributeName, MatrixItem>))]
[JsonSerializable(typeof(IImmutableValueDictionary<AttributeName, IImmutableValueList<int>>))]
[JsonSerializable(typeof(ImmutableValueDictionary<AttributeName, IImmutableValueList<int>>))]
[JsonSerializable(typeof(IImmutableValueDictionary<Extensible<AttributeName>, int>))]
[JsonSerializable(typeof(ImmutableValueDictionary<Extensible<AttributeName>, int>))]
[JsonSerializable(typeof(IImmutableValueDictionary<Extensible<AttributeName>, string>))]
[JsonSerializable(typeof(ImmutableValueDictionary<Extensible<AttributeName>, string>))]
[JsonSerializable(typeof(IImmutableValueDictionary<Extensible<AttributeName>, object>))]
[JsonSerializable(typeof(ImmutableValueDictionary<Extensible<AttributeName>, object>))]
[JsonSerializable(typeof(IImmutableValueDictionary<Extensible<AttributeName>, Coin>))]
[JsonSerializable(typeof(ImmutableValueDictionary<Extensible<AttributeName>, Coin>))]
[JsonSerializable(typeof(IImmutableValueDictionary<Extensible<AttributeName>, MatrixItem>))]
[JsonSerializable(typeof(ImmutableValueDictionary<Extensible<AttributeName>, MatrixItem>))]
[JsonSerializable(typeof(IImmutableValueDictionary<Extensible<AttributeName>, IImmutableValueList<int>>))]
[JsonSerializable(typeof(ImmutableValueDictionary<Extensible<AttributeName>, IImmutableValueList<int>>))]
[JsonSerializable(typeof(MatrixHolder))]
internal sealed partial class LegacyMatrixJsonContext : JsonSerializerContext;
