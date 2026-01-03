using GuildWars2.Hero;

namespace GuildWars2.Pvp.Amulets;

/// <summary>Information about a PvP amulet, which replaces armor bonuses in PvP.</summary>
[DataTransferObject]
public sealed record Amulet
{
    /// <summary>The amulet ID.</summary>
    public required int Id { get; init; }

    /// <summary>The amulet name.</summary>
    public required string Name { get; init; }

    /// <summary>The URL of the amulet icon.</summary>
    [Obsolete("Use IconUrl instead.")]
    public required string IconHref { get; init; }

    /// <summary>The URL of the amulet icon as a Uri.</summary>
    public required Uri IconUrl { get; init; }

    /// <summary>The effective attributes of the amulet.</summary>
    public required IDictionary<AttributeName, int> Attributes { get; init; }
}
