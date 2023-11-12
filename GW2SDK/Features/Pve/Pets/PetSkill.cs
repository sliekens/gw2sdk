namespace GuildWars2.Pve.Pets;

[PublicAPI]
[DataTransferObject]
public sealed record PetSkill
{
    public required int Id { get; init; }
}
