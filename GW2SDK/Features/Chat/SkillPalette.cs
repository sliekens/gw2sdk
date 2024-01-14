namespace GuildWars2.Chat;

/// <summary>Information about selected skills.</summary>
/// <param name="Heal">The heal skill palette ID.</param>
/// <param name="Utility1">The first utility skill palette ID.</param>
/// <param name="Utility2">The second utility skill palette ID.</param>
/// <param name="Utility3">The third utility skill palette ID.</param>
/// <param name="Elite">The elite skill palette ID.</param>
[PublicAPI]
public record struct SkillPalette(int? Heal, int? Utility1, int? Utility2, int? Utility3, int? Elite);
