namespace GuildWars2.Pve.Pets;

[PublicAPI]
[DataTransferObject]
public sealed record Pet
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    /// <summary>The URL of the pet icon.</summary>
    public required string IconHref { get; init; }

    public required IReadOnlyCollection<PetSkill> Skills { get; init; }
}
