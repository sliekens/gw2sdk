using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a Black Lion chest, which opens its own user interface when used.</summary>
[PublicAPI]
[JsonConverter(typeof(BlackLionChestJsonConverter))]
public sealed record BlackLionChest : Container;
