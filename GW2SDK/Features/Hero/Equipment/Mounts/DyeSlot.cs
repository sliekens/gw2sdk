namespace GuildWars2.Hero.Equipment.Mounts;

/// <summary>Information about a dye slot on a mount.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record DyeSlot
{
    /// <summary>The dye ID.</summary>
    public required int DyeId { get; init; }

    /// <summary>The material of the dye slot.</summary>
    public required Material Material { get; init; }
}
