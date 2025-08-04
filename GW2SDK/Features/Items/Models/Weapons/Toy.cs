using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a costume brawl weapon, which existed before the introduction of Novelties.</summary>
/// <remarks>Toys can no longer be equipped, a Black Lion Trader (Armorsmith) will exchange toy weapons for gizmos that
/// summon a bundle with the same functionality.</remarks>
[JsonConverter(typeof(ToyJsonConverter))]
public sealed record Toy : Weapon;
