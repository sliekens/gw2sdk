namespace GuildWars2.Wvw.Matches;

/// <summary>Information about the worlds participating in a World vs. World match.</summary>
[DataTransferObject]
public sealed record Worlds
{
    /// <summary>The ID of world playing for the red team.</summary>
    public required int Red { get; init; }

    /// <summary>The ID of world playing for the blue team.</summary>
    public required int Blue { get; init; }

    /// <summary>The ID of world playing for the green team.</summary>
    public required int Green { get; init; }
}
