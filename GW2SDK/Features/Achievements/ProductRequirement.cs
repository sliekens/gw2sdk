namespace GuildWars2.Achievements;

[PublicAPI]
[DataTransferObject]
public sealed record ProductRequirement
{
    public required ProductName Product { get; init; }

    public required AccessCondition Condition { get; init; }
}
