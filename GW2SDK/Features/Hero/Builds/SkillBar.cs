namespace GuildWars2.Hero.Builds;

/// <summary>Information about selected skills in a build.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SkillBar
{
    /// <summary>The ID of the heal skill or <c>null</c> if no heal skill was selected.</summary>
    public required int? HealSkillId { get; init; }

    /// <summary>The ID of the first utility skill or <c>null</c> if no utility skill was selected.</summary>
    public required int? UtilitySkillId { get; init; }

    /// <summary>The ID of the second utility skill or <c>null</c> if no utility skill was selected.</summary>
    public required int? UtilitySkillId2 { get; init; }

    /// <summary>The ID of the third utility skill or <c>null</c> if no utility skill was selected.</summary>
    public required int? UtilitySkillId3 { get; init; }

    /// <summary>The ID of the elite skill or <c>null</c> if no elite skill was selected.</summary>
    public required int? EliteSkillId { get; init; }
}
