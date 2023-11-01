namespace GuildWars2.Skills;

/// <summary>Information about the selected skills of a pet.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record PetSkillBar
{
    /// <summary>The IDs of the pet skills for ground combat. Empty skill slots are represented as <c>null</c>/</summary>
    public required IReadOnlyList<int?> SkillIds { get; init; }

    /// <summary>The IDs of the pet skills for underwater combat. Empty skill slots are represented as <c>null</c>/</summary>
    public required IReadOnlyList<int?> AquaticSkillIds { get; init; }
}
