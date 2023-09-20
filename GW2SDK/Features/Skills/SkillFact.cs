namespace GuildWars2.Skills;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record SkillFact
{
    public required string Text { get; init; }

    public required string Icon { get; init; }
}
