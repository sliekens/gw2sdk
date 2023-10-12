namespace GuildWars2.BuildStorage;

[PublicAPI]
[DataTransferObject]
public sealed record BuildTab
{
    /// <summary>The number of the current tab.</summary>
    public required int Tab { get; init; }

    /// <summary>Whether this is the active build tab.</summary>
    /// <remarks>Expect API updates to be delayed by a few minutes when switching the active build tab in the game.</remarks>
    public required bool IsActive { get; init; }

    /// <summary>The selected skills and traits on the current build tab.</summary>
    public required Build Build { get; init; }
}
