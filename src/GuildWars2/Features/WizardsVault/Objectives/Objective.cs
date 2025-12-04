namespace GuildWars2.WizardsVault.Objectives;

/// <summary>Information about a Wizard's Vault objective.</summary>
[DataTransferObject]
public sealed record Objective
{
    /// <summary>The objective ID.</summary>
    public required int Id { get; init; }

    /// <summary>The title of the objective.</summary>
    public required string Title { get; init; }

    /// <summary>The track that the objective is associated with.</summary>
    public required Extensible<ObjectiveTrack> Track { get; init; }

    /// <summary>The amount of Astral Acclaim that is awarded for completing the objective.</summary>
    public required int RewardAcclaim { get; init; }
}
