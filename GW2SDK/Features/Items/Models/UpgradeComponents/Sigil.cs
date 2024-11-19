using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a sigil, which is used to upgrade weapons.</summary>
[PublicAPI]
[JsonConverter(typeof(SigilJsonConverter))]
public sealed record Sigil : UpgradeComponent;
