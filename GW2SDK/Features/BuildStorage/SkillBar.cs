namespace GuildWars2.BuildStorage;

[PublicAPI]
[DataTransferObject]
public sealed record SkillBar
{
    public required int? Heal { get; init; }

    // Always length 3
    public required IReadOnlyCollection<int?> Utilities { get; init; }

    public required int? Elite { get; init; }
}
