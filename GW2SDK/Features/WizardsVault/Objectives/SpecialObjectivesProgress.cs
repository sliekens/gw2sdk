namespace GuildWars2.WizardsVault.Objectives;

/// <summary>Information about progress towards completing special Wizard's Vault objectives.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SpecialObjectivesProgress
{
    /// <summary>The special objectives and the player's progress.</summary>
    public required IReadOnlyList<ObjectiveProgress> Objectives { get; init; }
}
