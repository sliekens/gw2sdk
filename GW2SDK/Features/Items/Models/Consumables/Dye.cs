namespace GuildWars2.Items;

/// <summary>Information about a dye which unlocks a color when consumed.</summary>
[PublicAPI]
public sealed record Dye : Unlocker
{
    /// <summary>The ID of the color that is unlocked when this dye is consumed.</summary>
    public required int ColorId { get; init; }
}
