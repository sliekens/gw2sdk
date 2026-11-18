using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Hero.Builds;
using GuildWars2.Tests.Common;

namespace GuildWars2.ArchitectureTests;

[Feature("Architecture")]
[ClassDataSource<AssemblyFixture>(Shared = SharedType.PerTestSession)]
public class JsonConverterTest(AssemblyFixture fixture)
{
    [Test]
    public async Task AllEnumsShouldHaveJsonConverterAttribute()
    {
        IEnumerable<Type> enumTypes = fixture.ExportedEnums.Where(t => t is { IsPublic: true, Namespace: not null } && t.Namespace.StartsWith("GuildWars2", StringComparison.Ordinal));
        using (Assert.Multiple())
        {
            foreach (Type enumType in enumTypes)
            {
                bool hasJsonConverterAttribute = enumType.GetCustomAttributes(typeof(JsonConverterAttribute), false).Length != 0;
                await Assert.That(hasJsonConverterAttribute).IsTrue().Because($"Enum {enumType.Name} does not have a JsonConverterAttribute.");
            }
        }
    }

    [Test]
    public async Task JsonConverterAttributesUseAPublicConverter()
    {
        // A consumer source generator constructs the converter type named by [JsonConverter]. Internal converters
        // are rejected with SYSLIB1220 (https://github.com/sliekens/gw2sdk/issues/520).
        JsonSerializerOptions options = new();
        ModelJsonConverterFactory factory = new();
        using (Assert.Multiple())
        {
            foreach (Type type in typeof(ModelJsonConverterFactory).Assembly.GetExportedTypes())
            {
                if (type.GetCustomAttribute<JsonConverterAttribute>(inherit: false)?.ConverterType is not { } converterType)
                {
                    continue;
                }

                await Assert.That(converterType.IsPublic).IsTrue().Because($"{type.FullName} uses {converterType.FullName}, which a consumer source generator cannot construct.");
                if (converterType != typeof(ModelJsonConverterFactory) || type.IsGenericTypeDefinition)
                {
                    continue;
                }

                await Assert.That(factory.CanConvert(type)).IsTrue().Because(type.ToString());
                JsonConverter created = factory.CreateConverter(type, options);
                await Assert.That(created is JsonConverterFactory).IsFalse().Because(type.ToString());
                await Assert.That(created.CanConvert(type)).IsTrue().Because(type.ToString());
            }

            await Assert.That(factory.CanConvert(typeof(Extensible<WeaponType>))).IsTrue();
            JsonConverter extensible = factory.CreateConverter(typeof(Extensible<WeaponType>), options);
            await Assert.That(extensible is JsonConverterFactory).IsFalse();
            await Assert.That(extensible.CanConvert(typeof(Extensible<WeaponType>))).IsTrue();
        }
    }
}
