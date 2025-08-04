namespace GuildWars2.Pve.Pets;

/// <summary>Information about a pet skill.</summary>
[DataTransferObject]
public sealed record PetSkill
{
    /// <summary>The skill ID.</summary>
    public required int Id { get; init; }
}
