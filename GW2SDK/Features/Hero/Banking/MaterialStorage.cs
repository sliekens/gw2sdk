namespace GuildWars2.Hero.Banking;

/// <summary>Information about the current account's material storage.</summary>
[PublicAPI]
public sealed record MaterialStorage
{
    /// <summary>The materials in the material storage.</summary>
    public required IReadOnlyList<MaterialSlot> Materials { get; init; }
}
