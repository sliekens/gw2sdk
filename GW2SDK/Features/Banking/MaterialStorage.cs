namespace GuildWars2.Banking;

/// <summary>Information about the current account's material storage.</summary>
[PublicAPI]
public sealed record MaterialStorage
{
    public required IReadOnlyList<MaterialSlot> Materials { get; init; }
}
