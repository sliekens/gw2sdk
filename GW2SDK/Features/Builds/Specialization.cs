namespace GuildWars2.Builds;

/// <summary>Information about a specialization or elite specialization.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Specialization
{
    /// <summary>The specialization ID.</summary>
    public required int Id { get; init; }

    /// <summary>The specialization name.</summary>
    public required string Name { get; init; }

    /// <summary>The profession that can use this specialization.</summary>
    public required ProfessionName Profession { get; init; }

    /// <summary>Whether this is an elite specialization.</summary>
    public required bool Elite { get; init; }

    /// <summary>The IDs of the traits which are always active.</summary>
    public required IReadOnlyCollection<int> MinorTraitIds { get; init; }

    /// <summary>The IDs of the traits which can be selected.</summary>
    public required IReadOnlyCollection<int> MajorTraitIds { get; init; }

    /// <summary>If this is an <see cref="Elite" /> specialization, this is the ID of the trait which lets the player wield the
    /// elite specialization's weapon.</summary>
    public required int? WeaponTraitId { get; init; }

    /// <summary>The specialization icon.</summary>
    public required string IconHref { get; init; }

    /// <summary>The URL of the background image for this specialization.</summary>
    public required string BackgroundHref { get; init; }

    /// <summary>If this is an <see cref="Elite" /> specialization, this is the URL of the specialization's large icon.</summary>
    public required string ProfessionIconBig { get; init; }

    /// <summary>If this is an <see cref="Elite" /> specialization, this is the URL of the specialization's small icon.</summary>
    public required string ProfessionIcon { get; init; }
}
