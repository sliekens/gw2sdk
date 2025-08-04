using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a mount adoption or select license.</summary>
[JsonConverter(typeof(MountLicenseJsonConverter))]
public sealed record MountLicense : Consumable;
