namespace GuildWars2.Skills;

/// <summary>Information about selected skills in a build.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SkillBar
{
    /// <summary>The ID of the heal skill or <c>null</c> if no heal skill was selected.</summary>
    public required int? HealSkillId { get; init; }

    /// <summary>The IDs of the selected utility skills. This list is always length 3 because there are 3 utility skill slots
    /// per build. Empty skill slots are represented as <c>null</c>.</summary>
    public required IReadOnlyList<int?> UtilitySkillIds { get; init; }

    /// <summary>The ID of the elite skill or <c>null</c> if no elite skill was selected.</summary>
    public required int? EliteSkillId { get; init; }
}
