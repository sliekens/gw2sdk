using GuildWars2.Builds.Skills.Facts;

namespace GuildWars2.Builds.Skills;

/// <summary>A precondition for a <see cref="Buff" />. The effect is only applied when the precondition is met.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record BuffPrefix
{
    /// <summary>The buff effect. I think this is always "Apply Buff/Condition".</summary>
    public required string Text { get; init; }

    /// <summary>The precondition as it appears in the tooltip before the buff icon.</summary>
    public required string Icon { get; init; }

    /// <summary>The status is a summary of the precondition. For example: "Fire Attunement" indicates the buff is applied if
    /// the skill is used while Fire Attunement is active.</summary>
    public required string Status { get; init; }

    /// <summary>The longer description of the precondition. For example: "Cast fire spells."</summary>
    public required string Description { get; init; }
}
