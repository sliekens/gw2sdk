using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a consumable which summons an NPC when consumed, for example Trading Post Express.</summary>
[JsonConverter(typeof(ContractNpcJsonConverter))]
public sealed record ContractNpc : Consumable;
