namespace GuildWars2.WizardsVault.Objectives;

/// <summary>Information about progress towards completing weekly Wizard's Vault objectives.</summary>
[DataTransferObject]
public sealed record WeeklyObjectivesProgress
{
    /// <summary>The amount of weekly objectives completed.</summary>
    public required int Progress { get; init; }

    /// <summary>The amount of completed objectives required to claim the weekly completion reward.</summary>
    public required int Goal { get; init; }

    /// <summary>The item ID of the completion reward.</summary>
    public required int RewardItemId { get; init; }

    /// <summary>The amount of Astral Acclaim that is awarded for weekly completion.</summary>
    public required int RewardAcclaim { get; init; }

    /// <summary>Whether the weekly completion reward was claimed.</summary>
    public required bool Claimed { get; init; }

    /// <summary>The weekly objectives and the player's progress.</summary>
    public required IReadOnlyList<ObjectiveProgress> Objectives { get; init; }
}
