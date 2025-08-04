namespace GuildWars2.Items;

/// <summary>Information about a gizmo which summons an NPC when used, for example Personal Trader Express.</summary>
/// <remarks>Rentable contract NPCs have unlimited uses, but only one can be out at a time.</remarks>
public sealed record RentableContractNpc : Gizmo;
