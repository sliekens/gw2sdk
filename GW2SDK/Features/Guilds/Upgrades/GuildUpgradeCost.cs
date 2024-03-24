namespace GuildWars2.Guilds.Upgrades;

/// <summary>Information about a guild upgrade cost. This class is the base type for all guild upgrades costs. Cast objects
/// of this type to a more specific type to access more properties.</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
public record GuildUpgradeCost;
