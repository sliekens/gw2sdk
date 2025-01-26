using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Mounts;

/// <summary>Information about a mount skin.</summary>
[PublicAPI]
[DataTransferObject]
[JsonConverter(typeof(MountSkinJsonConverter))]
public sealed record MountSkin
{
    /// <summary>The mount skin ID.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the mount skin.</summary>
    public required string Name { get; init; }

    /// <summary>The URL of the mount skin icon.</summary>
    public required string IconHref { get; init; }

    /// <summary>The dyes applied by default.</summary>
    public required IReadOnlyList<DyeSlot> DyeSlots { get; init; }

    /// <summary>The mount associated with the skin.</summary>
    public required Extensible<MountName> Mount { get; init; }
}
