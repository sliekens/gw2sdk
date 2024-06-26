﻿namespace GuildWars2.Hero.Training.Skills;

/// <summary>A short summary of a profession skill.</summary>
[PublicAPI]
public sealed record ProfessionSkillSummary : SkillSummary
{
    /// <summary>In case of stolen skills (Thief only), the name of the profession from which it can be stolen.</summary>
    public required Extensible<ProfessionName>? Source { get; init; }

    /// <summary>In case of elemental skills (Elementalist only), the name of the attunement that needs to be active.</summary>
    public required Extensible<Attunement>? Attunement { get; init; }
}
