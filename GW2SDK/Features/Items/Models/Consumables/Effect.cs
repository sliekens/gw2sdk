namespace GuildWars2.Items;

[PublicAPI]
[DataTransferObject]
public sealed record Effect
{
    public required string Name { get; init; }

    public required string Description { get; init; }

    public required TimeSpan Duration { get; init; }

    public required int ApplyCount { get; init; }

    /// <summary>The URL of the effect icon.</summary>
    public required string IconHref { get; init; }
}
