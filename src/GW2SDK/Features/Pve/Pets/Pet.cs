namespace GuildWars2.Pve.Pets;

/// <summary>Information about a ranger pet.</summary>
[DataTransferObject]
public sealed record Pet
{
    /// <summary>The pet ID.</summary>
    public required int Id { get; init; }

    /// <summary>The pet name.</summary>
    public required string Name { get; init; }

    /// <summary>The pet description.</summary>
    public required string Description { get; init; }

    /// <summary>The URL of the pet icon.</summary>
    [Obsolete("Use IconUrl instead.")]
    public required string IconHref { get; init; }

    /// <summary>The URL of the pet icon as a Uri.</summary>
    public required Uri IconUrl { get; init; }

    /// <summary>The pet skills.</summary>
    public required IReadOnlyList<PetSkill> Skills { get; init; }
}
