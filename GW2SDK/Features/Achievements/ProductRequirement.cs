namespace GuildWars2.Achievements;

/// <summary>Describes which product is required to access an achievement.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record ProductRequirement
{
    /// <summary>The name of an expansion.</summary>
    public required ProductName Product { get; init; }

    /// <summary>Indicates whether the expansion is required to access the achievement.</summary>
    public required AccessCondition Condition { get; init; }
}
