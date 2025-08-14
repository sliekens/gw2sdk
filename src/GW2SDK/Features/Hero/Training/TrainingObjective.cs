namespace GuildWars2.Hero.Training;

/// <summary>Information about a training objective such as a skill or trait. This type is the base type. Cast objects of
/// this type to a more specific type to access more properties.</summary>
[Inheritable]
[DataTransferObject]
public record TrainingObjective
{
    /// <summary>The skill points required to complete the objective.</summary>
    /// <remarks>The cost is expressed as the accumulated total number of hero points that need to be spent to complete this
    /// objective and any objectives before it. Subtract points already spent to complete previous objectives to get the real
    /// cost.</remarks>
    public required int Cost { get; init; }
}
