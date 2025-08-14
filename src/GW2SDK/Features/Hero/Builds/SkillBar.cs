namespace GuildWars2.Hero.Builds;

/// <summary>Information about selected skills in a build.</summary>
[DataTransferObject]
public sealed record SkillBar
{
    /// <summary>The ID of the heal skill or <c>null</c> if no heal skill was selected.</summary>
    public required int? HealSkillId { get; init; }

    /// <summary>The ID of the first utility skill or <c>null</c> if no utility skill was selected.</summary>
    public required int? UtilitySkillId1 { get; init; }

    /// <summary>The ID of the second utility skill or <c>null</c> if no utility skill was selected.</summary>
    public required int? UtilitySkillId2 { get; init; }

    /// <summary>The ID of the third utility skill or <c>null</c> if no utility skill was selected.</summary>
    public required int? UtilitySkillId3 { get; init; }

    /// <summary>The ID of the elite skill or <c>null</c> if no elite skill was selected.</summary>
    public required int? EliteSkillId { get; init; }

    /// <summary>Gets the IDs of the selected skills.</summary>
    /// <returns>The IDs of the selected skills.</returns>
    public IEnumerable<int> SelectedSkillIds()
    {
        if (HealSkillId.HasValue)
        {
            yield return HealSkillId.Value;
        }

        if (UtilitySkillId1.HasValue)
        {
            yield return UtilitySkillId1.Value;
        }

        if (UtilitySkillId2.HasValue)
        {
            yield return UtilitySkillId2.Value;
        }

        if (UtilitySkillId3.HasValue)
        {
            yield return UtilitySkillId3.Value;
        }

        if (EliteSkillId.HasValue)
        {
            yield return EliteSkillId.Value;
        }
    }
}
