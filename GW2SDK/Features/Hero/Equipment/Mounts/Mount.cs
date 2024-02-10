namespace GuildWars2.Hero.Equipment.Mounts;

[PublicAPI]
[DataTransferObject]
public sealed record Mount
{
    public required MountName Id { get; init; }

    public required string Name { get; init; }

    public required int DefaultSkin { get; init; }

    public required IReadOnlyCollection<int> SkinIds { get; init; }

    public required IReadOnlyCollection<SkillReference> Skills { get; init; }
}
