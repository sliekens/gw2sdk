namespace GuildWars2.Armory;

[PublicAPI]
public sealed record CharacterEquipment
{
    /// <summary>All the equipment in the current character's armory.</summary>
    public required IReadOnlyList<EquipmentItem>? Equipment { get; init; }
}
