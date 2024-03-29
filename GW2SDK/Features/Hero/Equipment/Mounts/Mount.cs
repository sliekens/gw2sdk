namespace GuildWars2.Hero.Equipment.Mounts;

/// <summary>Information about a mount.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Mount
{
    /// <summary>The mount ID.</summary>
    public required Extensible<MountName> Id { get; init; }

    /// <summary>The mount name.</summary>
    public required string Name { get; init; }

    /// <summary>The ID of the default mount skin.</summary>
    public required int DefaultSkinId { get; init; }

    /// <summary>The IDs of the mount skins associated with the mount.</summary>
    public required IReadOnlyCollection<int> SkinIds { get; init; }

    /// <summary>The skills that replace the skill bar when the mount is active.</summary>
    public required IReadOnlyCollection<SkillReference> Skills { get; init; }
}
