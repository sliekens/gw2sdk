using System.Drawing;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Authorization;
using GuildWars2.Collections;
using GuildWars2.Exploration.PointsOfInterest;
using GuildWars2.Guilds.Upgrades;
using GuildWars2.Hero;
using GuildWars2.Hero.Builds;
using GuildWars2.Hero.Builds.Facts;
using GuildWars2.Hero.Builds.Skills;
using GuildWars2.Hero.Training;
using GuildWars2.Hero.Training.Objectives;
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

    [Test]
    public async Task DataTransferObjectsWithDerivedTypesUsePolymorphicConverters()
    {
        // Flags stay per-type objects. They are not serialized through a Flags reference.
        // https://github.com/sliekens/gw2sdk/issues/522
        Type[] exported = typeof(ModelJsonConverterFactory).Assembly.GetExportedTypes();
        using (Assert.Multiple())
        {
            foreach (Type type in exported)
            {
                bool dataTransferObject = type.GetCustomAttributes(inherit: false).Any(attribute => attribute.GetType().Name == "DataTransferObjectAttribute");
                if (!dataTransferObject || type == typeof(Flags) || !exported.Any(other => other.BaseType == type))
                {
                    continue;
                }

                Type? converter = type.GetCustomAttribute<JsonConverterAttribute>(inherit: false)?.ConverterType;
                await Assert.That(converter).IsEqualTo(typeof(ModelJsonConverterFactory)).Because(type.ToString());
            }
        }
    }

    [Test]
    public async Task PolymorphicModelsKeepDerivedProperties()
    {
        SubtokenInfo subtoken = new()
        {
            Id = "abc",
            Name = "n",
            Permissions = new ImmutableValueList<Extensible<Permission>>([Permission.Account]),
            ExpiresAt = DateTimeOffset.UnixEpoch,
            IssuedAt = DateTimeOffset.UnixEpoch,
            Urls = null
        };
        string subtokenJson = JsonSerializer.Serialize<TokenInfo>(subtoken);
        await Assert.That(subtokenJson.Contains("\"$type\":\"subtoken_info\"", StringComparison.Ordinal)).IsTrue();
        await Assert.That(subtokenJson.Contains("\"expires_at\"", StringComparison.Ordinal)).IsTrue();
        await Assert.That(JsonSerializer.Deserialize<TokenInfo>(subtokenJson)).IsEqualTo(subtoken);

        SkillObjective objective = new() { Cost = 5, SkillId = 9 };
        await Assert.That(JsonSerializer.Deserialize<TrainingObjective>(JsonSerializer.Serialize<TrainingObjective>(objective))).IsEqualTo(objective);

        Number number = new() { Text = "Removed", IconUrl = null, Value = 2 };
        await Assert.That(JsonSerializer.Deserialize<Fact>(JsonSerializer.Serialize<Fact>(number))).IsEqualTo(number);

        Waypoint waypoint = new()
        {
            Name = "A",
            Id = 1,
            Floor = 2,
            Coordinates = new PointF(1.5f, -2.25f),
            ChatLink = "[&B]"
        };
        await Assert.That(JsonSerializer.Deserialize<PointOfInterest>(JsonSerializer.Serialize<PointOfInterest>(waypoint))).IsEqualTo(waypoint);

        GuildUpgradeCoinsCost cost = new() { Coins = new Coin(1, 2, 3) };
        await Assert.That(JsonSerializer.Deserialize<GuildUpgradeCost>(JsonSerializer.Serialize<GuildUpgradeCost>(cost))).IsEqualTo(cost);

        PrefixedBuff buff = new()
        {
            Text = "Might",
            IconUrl = new Uri("https://example.test/icon.png"),
            Duration = TimeSpan.FromSeconds(5),
            Status = "Might",
            Description = "Increased power",
            ApplyCount = 3,
            PrefixIconUrl = new Uri("https://example.test/prefix.png"),
            Precondition = "Fire Attunement"
        };
        await Assert.That(JsonSerializer.Deserialize<Fact>(JsonSerializer.Serialize<Fact>(buff))).IsEqualTo(buff);

        EliteSkill elite = new()
        {
            Id = 1,
            Name = "Elite",
            Facts = null,
            TraitedFacts = null,
            Description = "",
            IconUrl = null,
            ChatLink = "[&B]",
            Categories = [],
            SkillFlags = new() { GroundTargeted = true, NoUnderwater = false, Other = [] },
            WeaponType = WeaponType.Staff,
            Professions = [ProfessionName.Elementalist],
            Slot = SkillSlot.Elite,
            FlipSkillId = null,
            NextSkillId = null,
            PreviousSkillId = null,
            SpecializationId = 48,
            ToolbeltSkillId = null,
            Attunement = null,
            Cost = 5,
            BundleSkillIds = null,
            SubskillIds = null,
            TransformSkillIds = [1, 2]
        };
        await Assert.That(JsonSerializer.Deserialize<Skill>(JsonSerializer.Serialize<Skill>(elite))).IsEqualTo(elite);
    }
}
