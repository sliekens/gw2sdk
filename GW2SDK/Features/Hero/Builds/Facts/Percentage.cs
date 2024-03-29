﻿namespace GuildWars2.Hero.Builds.Facts;

/// <summary>A percentage of something as referenced by the <see cref="Fact.Text" />.</summary>
[PublicAPI]
public sealed record Percentage : Fact
{
    /// <summary>The percentage something referenced by the <see cref="Fact.Text" />. For example, if the text is "Health
    /// Threshold" then <see cref="Percent" /> is the health threshold for activating the skill behavior.</summary>
    public required double Percent { get; init; }
}
