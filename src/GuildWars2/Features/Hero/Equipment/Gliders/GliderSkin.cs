using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Gliders;

/// <summary>Information about a glider skin.</summary>
[DataTransferObject]
[JsonConverter(typeof(GliderSkinJsonConverter))]
public sealed record GliderSkin
{
    /// <summary>The glider skin ID.</summary>
    public required int Id { get; init; }

    /// <summary>The IDs of the items that unlock the glider skin when consumed.</summary>
    public required IReadOnlyCollection<int> UnlockItemIds { get; init; }

    /// <summary>The display order of the glider skin in the equipment panel.</summary>
    public required int Order { get; init; }

    /// <summary>The URL of the glider skin icon.</summary>
    public required Uri IconUrl { get; init; }

    /// <summary>The name of the glider skin.</summary>
    public required string Name { get; init; }

    /// <summary>The description of the glider skin.</summary>
    /// <remarks>Can be empty.</remarks>
    public required string Description { get; init; }

    /// <summary>The color IDs of the dyes applied by default.</summary>
    public required IReadOnlyList<int> DefaultDyeColorIds { get; init; }
}
