namespace GuildWars2.Skills;

[PublicAPI]
[DataTransferObject]
public sealed record SkillReference
{
    public required int Id { get; init; }

    public required Attunement? Attunement { get; init; }

    public required Transformation? Form { get; init; }
}
