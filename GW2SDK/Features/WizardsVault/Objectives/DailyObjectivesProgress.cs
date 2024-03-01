namespace GuildWars2.WizardsVault.Objectives;

/// <summary>Information about progress towards completing daily Wizard's Vault objectives.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record DailyObjectivesProgress
{
    /// <summary>The amount of daily objectives completed.</summary>
    public required int Progress { get; init; }

    /// <summary>The amount of completed objectives required to claim the daily completion reward.</summary>
    public required int Goal { get; init; }

    /// <summary>The item ID of the completion reward.</summary>
    public required int RewardItemId { get; init; }

    /// <summary>The amount of Astral Acclaim that is awarded for daily completion.</summary>
    public required int RewardAcclaim { get; init; }

    /// <summary>Whether the daily completion reward was claimed.</summary>
    public required bool Claimed { get; init; }

    /// <summary>The daily objectives and the player's progress.</summary>
    public required IReadOnlyList<ObjectiveProgress> Objectives { get; init; }
}
