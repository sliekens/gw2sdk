namespace GuildWars2.Masteries;

[PublicAPI]
[DataTransferObject]
public sealed record Mastery
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    /// <remarks>Can be empty.</remarks>
    public required string Requirement { get; init; }

    public required int Order { get; init; }

    public required string Background { get; init; }

    public required MasteryRegionName Region { get; init; }

    public required IReadOnlyCollection<MasteryLevel> Levels { get; init; }
}
