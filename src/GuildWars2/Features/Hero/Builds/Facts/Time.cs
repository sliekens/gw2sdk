namespace GuildWars2.Hero.Builds.Facts;

/// <summary>Describes the duration of an effect as referenced by the <see cref="Fact.Text" />.</summary>
public sealed record Time : Fact
{
    /// <summary>The duration of something referenced by <see cref="Fact.Text" />.</summary>
    public required TimeSpan Duration { get; init; }
}
