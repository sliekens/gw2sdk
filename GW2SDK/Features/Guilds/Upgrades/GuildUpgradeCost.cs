namespace GuildWars2.Guilds.Upgrades;

/// <summary>Information about a guild upgrade cost. This class is the base type for all guild upgrades costs. Cast costs
/// to a more derived type to access their properties</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
public record GuildUpgradeCost;
