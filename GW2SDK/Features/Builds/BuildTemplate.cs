namespace GuildWars2.Builds;

/// <summary>Information about a build template on the character.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record BuildTemplate
{
    /// <summary>The tab number of the current template.</summary>
    public required int TabNumber { get; init; }

    /// <summary>Whether this is the active build template.</summary>
    /// <remarks>Expect there to be a delay after switching tabs in the game before this is reflected in the API.</remarks>
    public required bool IsActive { get; init; }

    /// <summary>The selected skills and traits for the current build template.</summary>
    public required Build Build { get; init; }
}
