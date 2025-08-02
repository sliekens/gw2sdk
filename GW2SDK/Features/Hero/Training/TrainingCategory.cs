using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Training;

/// <summary>The categories of training.</summary>
[PublicAPI]
[DefaultValue(None)]
[JsonConverter(typeof(TrainingCategoryJsonConverter))]
public enum TrainingCategory
{
    /// <summary>No specific category or unknown category.</summary>
    None,

    /// <summary>Skill training tracks.</summary>
    Skills,

    /// <summary>Specialization training tracks (traits).</summary>
    Specializations,

    /// <summary>Elite specialization training tracks (skills and traits).</summary>
    EliteSpecializations
}
