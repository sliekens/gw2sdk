using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a focus.</summary>
[JsonConverter(typeof(FocusJsonConverter))]
public sealed record Focus : Weapon;
