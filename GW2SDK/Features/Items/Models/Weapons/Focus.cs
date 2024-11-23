using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a focus.</summary>
[PublicAPI]
[JsonConverter(typeof(FocusJsonConverter))]
public sealed record Focus : Weapon;
