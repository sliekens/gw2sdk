using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a build storage expansion, which adds spaces to the account's Build Storage when consumed.</summary>
/// <remarks>Build Storage is the library where already existing builds may be saved, referenced, and copied by all
/// characters on the account.</remarks>
[PublicAPI]
[JsonConverter(typeof(BuildStorageExpansionJsonConverter))]
public sealed record BuildStorageExpansion : Unlocker;
