namespace GuildWars2.Hero.Builds.Facts;

/// <summary>A percentage of something as referenced by the <see cref="Fact.Text" />.</summary>
public sealed record Percentage : Fact
{
    /// <summary>The percentage something referenced by the <see cref="Fact.Text" />. For example, if the text is "Health
    /// Threshold" then <see cref="Percent" /> is the health threshold for activating the skill behavior.</summary>
    /// <remarks>This value is expressed as a whole number percentage (e.g., 75 for 75%), not as a fractional value (not 0.75).</remarks>
    public required double Percent { get; init; }
}
