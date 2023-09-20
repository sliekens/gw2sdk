namespace GuildWars2.BuildStorage;

[PublicAPI]
[DataTransferObject]
public sealed record PetSkillBar
{
    public required IReadOnlyCollection<int?> Terrestrial { get; init; }

    public required IReadOnlyCollection<int?> Aquatic { get; init; }
}
