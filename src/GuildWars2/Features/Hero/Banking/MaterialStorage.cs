namespace GuildWars2.Hero.Banking;

/// <summary>Information about the current account's material storage.</summary>
public sealed record MaterialStorage
{
    /// <summary>The materials in the material storage.</summary>
    public required IImmutableValueList<MaterialSlot> Materials { get; init; }
}
