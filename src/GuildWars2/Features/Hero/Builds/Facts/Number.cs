namespace GuildWars2.Hero.Builds.Facts;

/// <summary>Describes a number of something as referenced by the <see cref="Fact.Text" />.</summary>
public sealed record Number : Fact
{
    /// <summary>The number of something referenced by the <see cref="Fact.Text" />. For example, if the text is "Conditions
    /// Removed" then <see cref="Value" /> is the number of conditions removed.</summary>
    public required int Value { get; init; }
}
