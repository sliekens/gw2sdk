using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a small bundle (one-handed), which is a weapon that replaces your skills when equipped.
/// Bundles are acquired by picking them up in the open world, summoned by using a gizmo or obtained from NPCs.</summary>
[JsonConverter(typeof(SmallBundleJsonConverter))]
public sealed record SmallBundle : Weapon;
