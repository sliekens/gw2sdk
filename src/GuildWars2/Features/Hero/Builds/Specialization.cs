namespace GuildWars2.Hero.Builds;

/// <summary>Information about a specialization or elite specialization.</summary>
[DataTransferObject]
public sealed record Specialization
{
    /// <summary>The specialization ID.</summary>
    public required int Id { get; init; }

    /// <summary>The specialization name.</summary>
    public required string Name { get; init; }

    /// <summary>The profession that can use this specialization.</summary>
    public required Extensible<ProfessionName> Profession { get; init; }

    /// <summary>Whether this is an elite specialization.</summary>
    public required bool Elite { get; init; }

    /// <summary>The IDs of the traits which are always active.</summary>
    public required IReadOnlyList<int> MinorTraitIds { get; init; }

    /// <summary>The IDs of the traits which can be selected.</summary>
    public required IReadOnlyList<int> MajorTraitIds { get; init; }

    /// <summary>The ID of the trait which lets the player wield an elite specialization weapon.</summary>
    /// <remarks>Only <see cref="Elite" /> specializations have weapon traits.</remarks>
    public required int? WeaponTraitId { get; init; }

    /// <summary>The URL of the specialization icon.</summary>
    [Obsolete("Use IconUrl instead.")]
    public required string IconHref { get; init; }
    /// <summary>The URL of the specialization icon.</summary>
    public required Uri IconUrl { get; init; }

    /// <summary>The URL of the background image for this specialization.</summary>
    [Obsolete("Use BackgroundUrl instead.")]
    public required string BackgroundHref { get; init; }
    /// <summary>The URL of the background image for this specialization.</summary>
    public required Uri BackgroundUrl { get; init; }

    /// <summary>The URL of the large profession icon.</summary>
    /// <remarks>Only <see cref="Elite" /> specializations have profession icons.</remarks>
    [Obsolete("Use ProfessionBigIconUrl instead.")]
    public required string ProfessionBigIconHref { get; init; }
    /// <summary>The URL of the large profession icon.</summary>
    public required Uri? ProfessionBigIconUrl { get; init; }

    /// <summary>The URL of the small profession icon.</summary>
    /// <remarks>Only <see cref="Elite" /> specializations have profession icons.</remarks>
    [Obsolete("Use ProfessionIconUrl instead.")]
    public required string ProfessionIconHref { get; init; }
    /// <summary>The URL of the small profession icon.</summary>
    public required Uri? ProfessionIconUrl { get; init; }
}
