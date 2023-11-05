namespace GuildWars2.Hero.Builds.Facts;

/// <summary>The duration of the skill/trait.</summary>
[PublicAPI]
public sealed record Duration : Fact
{
    /// <summary>The duration.</summary>
    public required TimeSpan Length { get; init; }
}
