namespace GuildWars2.WizardsVault.Objectives;

/// <summary>Information about progress towards completing a Wizard's Vault objective.</summary>
[DataTransferObject]
public sealed record ObjectiveProgress
{
    /// <summary>The objective ID.</summary>
    public required int Id { get; init; }

    /// <summary>The title of the objective.</summary>
    public required string Title { get; init; }

    /// <summary>The track that the objective is associated with.</summary>
    public required Extensible<ObjectiveTrack> Track { get; init; }

    /// <summary>The amount of Astral Acclaim that is awarded for completing the objective.</summary>
    public required int RewardAcclaim { get; init; }

    /// <summary>The amount of progress made so far.</summary>
    public required int Progress { get; init; }

    /// <summary>The amount of progress required to complete the objective.</summary>
    public required int Goal { get; init; }

    /// <summary>Whether the objective reward was claimed.</summary>
    public required bool Claimed { get; init; }
}
