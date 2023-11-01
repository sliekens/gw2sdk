namespace GuildWars2.Skills.Facts;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record SkillFact
{
    public required string Text { get; init; }

    public required string Icon { get; init; }
}
