namespace GuildWars2.Chat;

/// <summary>Information about the selected legends for a Revenant build.</summary>
[PublicAPI]
public sealed record Legends
{
    /// <summary>The code of the active legend, or <c>null</c> if no legend was selected.</summary>
    public required int? ActiveTerrestrialLegend { get; init; }

    /// <summary>The code of the other selected legend, or <c>null</c> if no legend was selected.</summary>
    public required int? InactiveTerrestrialLegend { get; init; }

    /// <summary>The code of the first selected legend for underwater, or <c>null</c> if no legend was selected.</summary>
    public required int? ActiveAquaticLegend { get; init; }

    /// <summary>The code of the other selected legend for underwater, or <c>null</c> if no legend was selected.</summary>
    public required int? InactiveAquaticLegend { get; init; }

    /// <summary>The utility skills for the other, inactive terrestrial legend</summary>
    public required SkillPalette InactiveTerrestrialSkills { get; init; }

    /// <summary>The utility skills for the other, inactive aquatic legend</summary>
    public required SkillPalette InactiveAquaticSkills { get; init; }
}
