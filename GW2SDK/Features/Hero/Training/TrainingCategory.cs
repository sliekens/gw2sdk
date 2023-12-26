namespace GuildWars2.Hero.Training;

/// <summary>The categories of training.</summary>
[PublicAPI]
public enum TrainingCategory
{
    /// <summary>Skill training tracks.</summary>
    Skills = 1,

    /// <summary>Specialization training tracks (traits).</summary>
    Specializations,

    /// <summary>Elite specialization training tracks (skills and traits).</summary>
    EliteSpecializations
}
