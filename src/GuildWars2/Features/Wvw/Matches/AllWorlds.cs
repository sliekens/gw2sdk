namespace GuildWars2.Wvw.Matches;

/// <summary>Information about the worlds participating in a World vs. World match.</summary>
[DataTransferObject]
public sealed record AllWorlds
{
    /// <summary>The IDs of worlds playing for the red team.</summary>
    public required IImmutableValueList<int> Red { get; init; }

    /// <summary>The IDs of worlds playing for the blue team.</summary>
    public required IImmutableValueList<int> Blue { get; init; }

    /// <summary>The IDs of worlds playing for the green team.</summary>
    public required IImmutableValueList<int> Green { get; init; }
}
