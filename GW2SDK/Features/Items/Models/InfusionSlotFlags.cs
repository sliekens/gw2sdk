namespace GuildWars2.Items;

/// <summary>Modifiers for infusion slots.</summary>
[PublicAPI]
public sealed record InfusionSlotFlags
{
    /// <summary>Whether enchrichment can be used in the slot.</summary>
    public required bool Enrichment { get; init; }

    /// <summary>Whether stat infusions can be used in the slot.</summary>
    public required bool Infusion { get; init; }

    /// <summary>Other undocumented flags. If you find out what they mean, please open an issue or a pull request.</summary>
    public required IReadOnlyCollection<string> Other { get; init; }
}
