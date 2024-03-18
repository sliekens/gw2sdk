namespace GuildWars2.Items;

/// <summary>Information about an effect applied by a consumable.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Effect
{
    /// <summary>The name of the effect.</summary>
    public required string Name { get; init; }

    /// <summary>The description of the effect.</summary>
    public required string Description { get; init; }

    /// <summary>The duration of the effect.</summary>
    public required TimeSpan Duration { get; init; }

    /// <summary>The number of stacks of the effect applied.</summary>
    public required int ApplyCount { get; init; }

    /// <summary>The URL of the effect icon.</summary>
    public required string IconHref { get; init; }
}
