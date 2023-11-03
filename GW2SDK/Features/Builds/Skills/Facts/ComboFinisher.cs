namespace GuildWars2.Builds.Skills.Facts;

/// <summary>A combo finisher effect applied by the skill.</summary>
[PublicAPI]
public sealed record ComboFinisher : SkillFact
{
    /// <summary>How much change the skill has to apply the combo finisher effect. Expressed as a percentage, where 100 is 100%
    /// chance.</summary>
    public required int Percent { get; init; }

    /// <summary>The kind of combo finisher effect applied by the skill.</summary>
    public required ComboFinisherName FinisherName { get; init; }
}
